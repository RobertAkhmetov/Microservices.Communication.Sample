namespace Microservices.Communication.Sample.Service2.Application.Abstractions.Messaging.Features.Commands.SendBaseMessageToService1;

public sealed record SendBaseMessageToService1Result(
    Guid RequestId,
    string RequestMessage,
    string ResponseMessage,
    DateTimeOffset RequestedAtUtc,
    DateTimeOffset RespondedAtUtc);
