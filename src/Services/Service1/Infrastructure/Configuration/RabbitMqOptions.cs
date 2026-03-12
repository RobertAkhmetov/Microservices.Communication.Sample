namespace Microservices.Communication.Sample.Service1.Infrastructure.Configuration;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMq";

    public string HostName { get; init; } = "localhost";

    public int Port { get; init; } = 5672;

    public string UserName { get; init; } = "guest";

    public string Password { get; init; } = "guest";

    public string VirtualHost { get; init; } = "/";

    public string RequestQueueName { get; init; } = "service2.base-message.requests";

    public int ResponseTimeoutSeconds { get; init; } = 10;
}
