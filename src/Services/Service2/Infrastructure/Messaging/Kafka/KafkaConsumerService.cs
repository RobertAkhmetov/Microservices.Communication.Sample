using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service2.Application;

public class KafkaConsumerService : IConsumerService
{
    private readonly IConsumer<string, string> _stringConsumer;
    private readonly ILogger<KafkaConsumerService> _logger;
    private bool _disposed;


    public KafkaConsumerService(IConfiguration config, ILogger<KafkaConsumerService> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"],
            ClientId = config["Kafka:ClientId"] ?? "default-consumer",
            GroupId = config["Kafka:GroupId"] ?? "service2-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest, // С чего начинать: Earliest (с начала) или Latest (новые)
            EnableAutoCommit = true               // Автоматически подтверждать прочитанное (удобно для старта)
        };

        _stringConsumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
    }

    public void Dispose()
    {
        if (_disposed) return;

        //_stringConsumer.Flush(TimeSpan.FromSeconds(10));
        _stringConsumer.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    public Task<string> GetMessagesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}