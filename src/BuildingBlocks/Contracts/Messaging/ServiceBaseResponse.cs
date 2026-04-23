namespace Microservices.Communication.Sample.Contracts.Messaging;

public sealed record ServiceBaseResponse(
    Guid RequestId,
    string Message,
    DateTimeOffset ProcessedAtUtc);
