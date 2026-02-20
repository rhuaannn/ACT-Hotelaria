using ACT_Hotelaria.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.Extension;

public static class ApiBuilderExtension
{
    public static void ApplyMigrations<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<T>();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}