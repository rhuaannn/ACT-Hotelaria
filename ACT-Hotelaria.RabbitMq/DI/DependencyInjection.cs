using ACT_Hotelaria.RabbitMq.Message;
using Microsoft.Extensions.DependencyInjection;

namespace ACT_Hotelaria.RabbitMq.DI;

public static class DependencyInjection 
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddSingleton<IMessageServiceBus, RabbitMqServices>();
        services.AddHostedService<RabbitMqStartupService>();        
        return services;
    }
}