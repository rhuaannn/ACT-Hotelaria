using ACT_Hotelaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.Infra;

public class ACT_HotelariaDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    
    public ACT_HotelariaDbContext(DbContextOptions<ACT_HotelariaDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Hotelaria");
        builder.ApplyConfigurationsFromAssembly(typeof(ACT_HotelariaDbContext).Assembly);
    }

}