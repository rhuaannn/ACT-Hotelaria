using ACT_Hotelaria.Redis.Repository;
using ACT_Hotelaria.Redis.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ACT_Hotelaria.Redis.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddRedisInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");


        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
            options.InstanceName = "ACT-Hotelaria:";
        });

        services.AddScoped<ICaching, Caching>();

        return services;
    }
}