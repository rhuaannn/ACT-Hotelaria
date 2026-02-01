using ACT_Hotelaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.SqlServer;

public class ACT_HotelariaDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Dependent> Dependents { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Invoicing> Invoicings { get; set; }
    public DbSet<Product>  Products { get; set; }
    public DbSet<Consumption> Consumptions { get; set; }
    
    public ACT_HotelariaDbContext(DbContextOptions<ACT_HotelariaDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Hotelaria");
        builder.ApplyConfigurationsFromAssembly(typeof(ACT_HotelariaDbContext).Assembly);
    }

}