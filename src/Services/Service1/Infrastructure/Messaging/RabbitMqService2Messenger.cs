using System.Text.Json;
using Microservices.Communication.Sample.Contracts.Messaging;
using Microservices.Communication.Sample.Service1.Application.Abstractions.Messaging;
using Microservices.Communication.Sample.Service1.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Microservices.Communication.Sample.Service1.Infrastructure.Messaging;

internal sealed class RabbitMqService2Messenger(
    RabbitMqConnectionProvider connectionProvider,
    IOptions<RabbitMqOptions> options) : IService2Messenger
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    private readonly RabbitMqOptions _options = options.Value;

    public async Task<ServiceBaseResponse> RequestBaseMessageAsync(
        ServiceBaseRequest request,
        CancellationToken cancellationToken)
    {
        var connection = await connectionProvider.GetConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: _options.RequestQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        var replyQueue = await channel.QueueDeclareAsync(
            queue: string.Empty,
            durable: false,
            exclusive: true,
            autoDelete: true,
            arguments: null,
            cancellationToken: cancellationToken);

        var correlationId = request.RequestId.ToString("N");
        var responseSource = new TaskCompletionSource<ServiceBaseResponse>(TaskCreationOptions.RunContinuationsAsynchronously);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (_, delivery) =>
        {
            if (!string.Equals(delivery.BasicProperties.CorrelationId, correlationId, StringComparison.Ordinal))
            {
                return Task.CompletedTask;
            }

            try
            {
                var response = JsonSerializer.Deserialize<ServiceBaseResponse>(delivery.Body.Span, SerializerOptions);
                if (response is null)
                {
                    responseSource.TrySetException(new InvalidOperationException("Service2 response payload is empty."));
                }
                else
                {
                    responseSource.TrySetResult(response);
                }
            }
            catch (Exception exception)
            {
                responseSource.TrySetException(exception);
            }

            return Task.CompletedTask;
        };

        var consumerTag = await channel.BasicConsumeAsync(
            queue: replyQueue.QueueName,
            autoAck: true,
            consumer: consumer,
            cancellationToken: cancellationToken);

        var body = JsonSerializer.SerializeToUtf8Bytes(request, SerializerOptions);
        var basicProperties = new BasicProperties
        {
            CorrelationId = correlationId,
            ReplyTo = replyQueue.QueueName,
            ContentType = "application/json",
            Persistent = true
        };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _options.RequestQueueName,
            mandatory: false,
            basicProperties: basicProperties,
            body: body,
            cancellationToken: cancellationToken);

        try
        {
            return await responseSource.Task.WaitAsync(
                TimeSpan.FromSeconds(_options.ResponseTimeoutSeconds),
                cancellationToken);
        }
        finally
        {
            await channel.BasicCancelAsync(consumerTag, cancellationToken: cancellationToken);
        }
    }
}
