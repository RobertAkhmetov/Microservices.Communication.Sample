using System.Text.Json;
using Confluent.Kafka;
using Contracts.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service2.Infrastructure.Interfaces.Kafka;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly IProducer<string, string> _stringProducer;
    private readonly ILogger<KafkaProducerService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _disposed;

    public KafkaProducerService(IConfiguration config, ILogger<KafkaProducerService> logger)
    {
        _logger = logger;

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"],
            ClientId = config["Kafka:ClientId"] ?? "default-producer",
            Acks = Acks.All,  // Ждём подтверждения от всех реплик
            EnableIdempotence = true,  // Предотвращение дублирования сообщений
            MessageSendMaxRetries = 3,  // Число повторных попыток
            RetryBackoffMs = 1000  // Интервал между попытками
        };

        _stringProducer = new ProducerBuilder<string, string>(producerConfig).Build();
        _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public void Dispose()
    {
        if (_disposed) return;

        _stringProducer.Flush(TimeSpan.FromSeconds(10));
        _stringProducer.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    public async Task<DeliveryResult<string, string>> ProduceAsync(string topic, string key, string value)
    {
        try
        {
            return await _stringProducer.ProduceAsync(topic, new Message<string, string>
            {
                Key = key,
                Value = value,
                Timestamp = new Timestamp(DateTime.UtcNow)
            });
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex, "Ошибка при отправке сообщения в топик {Topic}. Ключ: {Key}", topic, key);
            throw;
        }
    }

    public async Task<DeliveryResult<string, TValue>> ProduceAsync<TValue>(string topic, string key, TValue value)
    {
        string serializedValue = JsonSerializer.Serialize(value, _jsonOptions);
        var result = await ProduceAsync(topic, key, serializedValue);

        // Адаптация типа результата для сохранения интерфейса
        return new DeliveryResult<string, TValue>
        {
            Topic = result.Topic,
            Partition = result.Partition,
            Offset = result.Offset,
            Timestamp = result.Timestamp,
            Message = new Message<string, TValue>
            {
                Key = result.Message.Key,
                // Value имитируется, реальный десериализованный объект тут не будет
                Headers = result.Message.Headers
            }
        };
    }

    public async Task<ServiceBaseResponse> RequestBaseMessageAsync(ServiceBaseRequest request, CancellationToken cancellationToken)
    {
        await ProduceAsync("service_1_to_service_2", request.RequestId.ToString(), request.Message);

        return new ServiceBaseResponse(Guid.CreateVersion7(), "пока заглушка типа ответил", DateTimeOffset.Now);
    }
}