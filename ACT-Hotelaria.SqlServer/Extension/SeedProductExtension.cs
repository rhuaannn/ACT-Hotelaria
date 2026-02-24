using ACT_Hotelaria.SqlServer;
using ACT_Hotelaria.Domain.Seed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class SeedProductExtension
{
    public static void ApplyMigrationsAndSeed(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        try
        {
            var context = services.GetRequiredService<ACT_HotelariaDbContext>();
            SeedProduct.SeedProductInsert(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<ACT_HotelariaDbContext>>();
            logger.LogError(ex, "Erro ao semear dados.");
        }
    }
}