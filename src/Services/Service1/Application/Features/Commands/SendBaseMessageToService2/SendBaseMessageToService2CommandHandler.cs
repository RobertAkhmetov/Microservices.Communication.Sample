using MediatR;
using Microservices.Communication.Sample.Contracts.Messaging;
using Microservices.Communication.Sample.Service1.Application.Abstractions.Messaging;
using Microservices.Communication.Sample.Service1.Domain.Entities;

namespace Microservices.Communication.Sample.Service1.Application.Features.SendBaseMessageToService2;

internal sealed class SendBaseMessageToService2CommandHandler(IService2Messenger service2Messenger)
    : IRequestHandler<SendBaseMessageToService2Command, SendBaseMessageToService2Result>
{
    public async Task<SendBaseMessageToService2Result> Handle(
        SendBaseMessageToService2Command request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ArgumentException("Message is required.", nameof(request.Message));
        }

        var message = new OutgoingMessage(Guid.CreateVersion7(), request.Message.Trim(), DateTimeOffset.UtcNow);

        var response = await service2Messenger.RequestBaseMessageAsync(
            new Service2BaseRequest(message.Id, message.Body, message.CreatedAtUtc),
            cancellationToken);

        return new SendBaseMessageToService2Result(
            response.RequestId,
            message.Body,
            response.Message,
            message.CreatedAtUtc,
            response.ProcessedAtUtc);
    }
}
