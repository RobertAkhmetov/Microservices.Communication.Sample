namespace Microservices.Communication.Sample.Service2.Domain.Entities;

public sealed class OutgoingMessage
{
    public OutgoingMessage(Guid id, string body, DateTimeOffset createdAtUtc)
    {
        Id = id;
        Body = body;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Id { get; }

    public string Body { get; }

    public DateTimeOffset CreatedAtUtc { get; }
}
