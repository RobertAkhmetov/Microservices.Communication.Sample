namespace Microservices.Communication.Sample.Contracts.Messaging;

public sealed record Service2BaseRequest(
    Guid RequestId,
    string Message,
    DateTimeOffset SentAtUtc);
