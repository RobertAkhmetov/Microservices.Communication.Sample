using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class KafkaConsumerService
{
    private readonly IConsumer<string, string> _stringConsumer;
    private readonly ILogger<KafkaConsumerService> _logger;

    public KafkaConsumerService(IConfiguration config, ILogger<KafkaConsumerService> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"],
            ClientId = config["Kafka:ClientId"] ?? "default-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest, // С чего начинать: Earliest (с начала) или Latest (новые)
            EnableAutoCommit = true               // Автоматически подтверждать прочитанное (удобно для старта)
        };

        _stringConsumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
    }

}