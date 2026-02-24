using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.SqlServer;

namespace ACT_Hotelaria.Domain.Seed;

public static class SeedProduct
{
    public static void SeedProductInsert(ACT_HotelariaDbContext context)
    {
        context.Database.EnsureCreated();
        if (context.Products.Any())
        {
            return;
        }

        var product1 = Product.Create("Cozumel", 10, 15);
        var product2 = Product.Create("Caipirinha", 10, 15);
        var product3 = Product.Create("Cerveja", 10, 15);
        context.Products.AddRange(product1);
        context.Products.AddRange(product2);
        context.Products.AddRange(product3);
        context.SaveChanges();
    } 
}