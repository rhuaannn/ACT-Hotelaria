using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.ConsumptionRepository.cs;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Domain.Repository.InvoicingRepository;
using ACT_Hotelaria.Domain.Repository.ProductRepository;
using ACT_Hotelaria.Domain.Repository.Reservation;
using ACT_Hotelaria.Domain.Repository.RoomRepository;
using ACT_Hotelaria.SqlServer.IdentityConfig;
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
        services.AddIdentityConfiguraton(configuration);
        services.AddScoped<IWriteOnlyClientRepository, ClientRepository>();
        services.AddScoped<IReadOnlyClientRepository, ClientRepository>();
        services.AddScoped<IReadOnlyReservationRepository, ReservationRepository>();
        services.AddScoped<IWriteOnlyReservationRepository, ReservationRepository>();
        services.AddScoped<IReadOnlyDependentRepository, DependentRepository>();
        services.AddScoped<IReadOnlyProductRepository, ProductRepository>();
        services.AddScoped<IWriteOnlyProductRepository, ProductRepository>();
        services.AddScoped<IWriteOnlyConsumptionRepository, ConsumptionRepository>();
        services.AddScoped<IWriteOnlyInvoiceRepository, InvoicingRepository>();
        services.AddScoped<IWriteOnlyRoomRepository, RoomRepository>();
        services.AddScoped<IReadOnlyRoomRepository, RoomRepository>();
        services.AddScoped<IReadOnlyConsumptionRepository, ConsumptionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

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
