using ACT_Hotelaria.Application.UseCase.Client;
using ACT_Hotelaria.Application.UseCase.Client.GetAll;
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
        return services;
    }
}