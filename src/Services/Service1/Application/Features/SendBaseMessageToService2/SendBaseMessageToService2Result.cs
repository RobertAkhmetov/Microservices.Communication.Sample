namespace Microservices.Communication.Sample.Service1.Application.Features.SendBaseMessageToService2;

public sealed record SendBaseMessageToService2Result(
    Guid RequestId,
    string RequestMessage,
    string ResponseMessage,
    DateTimeOffset RequestedAtUtc,
    DateTimeOffset RespondedAtUtc);
