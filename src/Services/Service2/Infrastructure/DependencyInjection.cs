using System.Collections;
using Microservices.Communication.Sample.Service2.Application.Abstractions.Messaging;
using Microservices.Communication.Sample.Service2.Infrastructure.Configuration;
using Microservices.Communication.Sample.Service2.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<IService1Messenger, RabbitMqService1Messenger>();

        // services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

        // var config = new ProducerConfig
        // {
        //     BootstrapServers = "localhost:9092",
        //     Debug = "broker,topic,msg"
        // };

        return services;
    }
}