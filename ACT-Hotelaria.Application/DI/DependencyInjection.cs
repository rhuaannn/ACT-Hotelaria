using ACT_Hotelaria.Application.UseCase.Client;
using ACT_Hotelaria.Application.UseCase.Client.GetAll;
using ACT_Hotelaria.Application.UseCase.Client.GetById;
using ACT_Hotelaria.Application.UseCase.Consumption;
using ACT_Hotelaria.Application.UseCase.Invoicing;
using ACT_Hotelaria.Application.UseCase.Product;
using ACT_Hotelaria.Application.UseCase.Reservation;
using ACT_Hotelaria.Application.UseCase.Reservation.GetAll;
using Microsoft.Extensions.DependencyInjection;

namespace ACT_Hotelaria.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterClientUseCase>();
        services.AddScoped<RegisterReservationUseCase>();
        services.AddScoped<GetAllClientUseCase>();
        services.AddScoped<GetAllReservationUseCase>();
        services.AddScoped<GetByIdClientUseCase>();
        services.AddScoped<RegisterProductUseCase>();
        services.AddScoped<RegisterConsumptionUseCase>();
        services.AddScoped<RegisterInvoicingUseCase>();
        return services;
    }
}