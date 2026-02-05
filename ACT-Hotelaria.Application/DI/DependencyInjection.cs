using System.Reflection;
using ACT_Hotelaria.Application.UseCase.Client;
using ACT_Hotelaria.Application.UseCase.Client.GetAll;
using ACT_Hotelaria.Application.UseCase.Client.GetById;
using ACT_Hotelaria.Application.UseCase.Consumption;
using ACT_Hotelaria.Application.UseCase.Invoicing;
using ACT_Hotelaria.Application.UseCase.Product;
using ACT_Hotelaria.Application.UseCase.Reservation;
using ACT_Hotelaria.Application.UseCase.Reservation.GetAll;
using ACT_Hotelaria.Application.UseCase.Room;
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

        });

        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}