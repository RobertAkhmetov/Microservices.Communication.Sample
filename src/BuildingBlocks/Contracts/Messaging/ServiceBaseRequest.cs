namespace Contracts.Messaging;

public sealed record ServiceBaseRequest(
    Guid RequestId,
    string Message,
    DateTimeOffset SentAtUtc);
