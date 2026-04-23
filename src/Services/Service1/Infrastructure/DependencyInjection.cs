using Confluent.Kafka;
using Microservices.Communication.Sample.Service1.Application.Abstractions.Messaging;
using Microservices.Communication.Sample.Service1.Infrastructure.Configuration;
using Microservices.Communication.Sample.Service1.Infrastructure.Interfaces.Kafka;
using Microservices.Communication.Sample.Service1.Infrastructure.Messaging.Kafka;
using Microservices.Communication.Sample.Service1.Infrastructure.Messaging.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Communication.Sample.Service1.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.SectionName))
            .ValidateOnStart();

        services.AddSingleton<RabbitMqConnectionProvider>();
        services.AddScoped<IService2Messenger, RabbitMqService2Messenger>();

        services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            Debug = "broker,topic,msg"
        };


        return services;
    }
}
