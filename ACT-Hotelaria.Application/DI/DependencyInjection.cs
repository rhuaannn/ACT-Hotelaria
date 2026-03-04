using System.Net;
using System.Reflection;
using ACT_Hotelaria.Application.Abstract.Behavior;
using ACT_Hotelaria.Domain.DomainNotification;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
        services.AddScoped<NotificationContext>();
        return services;
    }
}