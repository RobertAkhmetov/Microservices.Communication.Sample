using Microservices.Communication.Sample.Service1.Application.Abstractions.Messaging;
using Microservices.Communication.Sample.Service1.Infrastructure.Configuration;
using Microservices.Communication.Sample.Service1.Infrastructure.Messaging;
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

        return services;
    }
}
