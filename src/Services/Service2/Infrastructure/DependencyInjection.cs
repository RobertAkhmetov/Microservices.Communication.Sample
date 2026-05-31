using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service2.Application;
using Service2.Application.Abstractions.Messaging;
using Service2.Infrastructure.Configuration;
using Service2.Infrastructure.Messaging;

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

        services.AddScoped<IConsumerService, KafkaConsumerService>();

        // services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

        // var config = new ProducerConfig
        // {
        //     BootstrapServers = "localhost:9092",
        //     Debug = "broker,topic,msg"
        // };

        return services;
    }
}