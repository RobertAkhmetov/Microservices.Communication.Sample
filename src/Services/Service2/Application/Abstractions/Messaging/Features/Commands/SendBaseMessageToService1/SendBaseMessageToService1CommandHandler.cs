using MediatR;
using Microservices.Communication.Sample.Contracts.Messaging;
using Microservices.Communication.Sample.Service2.Application.Abstractions.Messaging;
using Microservices.Communication.Sample.Service2.Domain.Entities;

namespace Microservices.Communication.Sample.Service2.Application.Abstractions.Messaging.Features.Commands.SendBaseMessageToService1;

internal sealed class SendBaseMessageToService1CommandHandler(IService1Messenger service2Messenger)
    : IRequestHandler<SendBaseMessageToService1Command, SendBaseMessageToService1Result>
{
    public async Task<SendBaseMessageToService1Result> Handle(
        SendBaseMessageToService1Command request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ArgumentException("Message is required.", nameof(request.Message));
        }

        var message = new OutgoingMessage(Guid.CreateVersion7(), request.Message.Trim(), DateTimeOffset.UtcNow);

        var response = await service2Messenger.RequestBaseMessageAsync(
            new ServiceBaseRequest(message.Id, message.Body, message.CreatedAtUtc),
            cancellationToken);

        return new SendBaseMessageToService1Result(
            response.RequestId,
            message.Body,
            response.Message,
            message.CreatedAtUtc,
            response.ProcessedAtUtc);
    }
}
