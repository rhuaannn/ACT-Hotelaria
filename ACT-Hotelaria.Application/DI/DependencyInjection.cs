using ACT_Hotelaria.Application.UseCase.Client;
using Microsoft.Extensions.DependencyInjection;

namespace ACT_Hotelaria.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterClientUseCase>();
        return services;
    }
}