namespace Microservices.Communication.Sample.Service2.Application.Abstractions.Messaging.Features.Commands.SendBaseMessageToService2;

public sealed record SendBaseMessageToService2Result(
    Guid RequestId,
    string RequestMessage,
    string ResponseMessage,
    DateTimeOffset RequestedAtUtc,
    DateTimeOffset RespondedAtUtc);
