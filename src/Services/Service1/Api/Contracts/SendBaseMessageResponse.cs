namespace Microservices.Communication.Sample.Service1.Api.Contracts;

public sealed record SendBaseMessageResponse(
    Guid RequestId,
    string RequestMessage,
    string ResponseMessage,
    DateTimeOffset RequestedAtUtc,
    DateTimeOffset RespondedAtUtc);
