using System.Reflection;
using ACT_Hotelaria.Application.Abstract.Behavior;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ACT_Hotelaria.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

        });

        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}