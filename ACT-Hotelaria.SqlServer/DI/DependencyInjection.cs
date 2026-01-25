using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.cs.Reservation;
using ACT_Hotelaria.SqlServer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ACT_Hotelaria.SqlServer.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer(configuration);
        services.AddScoped<IWriteOnlyClientRepository, ClientRepository>();
        services.AddScoped<IReadOnlyClientRepository, ClientRepository>();
        services.AddScoped<IReadOnlyReservationRepository, ReservationRepository>();
        services.AddScoped<IWriteOnlyReservationRepository, ReservationRepository>();
        
        return services;
    } 
    private static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ACT_HotelariaDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}
