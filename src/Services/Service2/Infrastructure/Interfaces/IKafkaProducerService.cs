using Confluent.Kafka;

namespace Service2.Infrastructure.Interfaces.Kafka;

public interface IKafkaProducerService
{
    Task<DeliveryResult<string, string>> ProduceAsync(string topic, string key, string value);
    Task<DeliveryResult<string, TValue>> ProduceAsync<TValue>(string topic, string key, TValue value);
    void Dispose();
}
