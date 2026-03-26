using System.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static ICollection AddInfrastructure()
    {
        services
            .AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.SectionName))
            .ValidateOnStart();

        services.AddSingleton<RabbitMqConnectionProvider>();
        //services.AddScoped<IService2Messenger, RabbitMqService2Messenger>();

        return services;
    }
}