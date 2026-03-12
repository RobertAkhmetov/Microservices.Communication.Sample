namespace Microservices.Communication.Sample.Contracts.Messaging;

public sealed record Service2BaseResponse(
    Guid RequestId,
    string Message,
    DateTimeOffset ProcessedAtUtc);
