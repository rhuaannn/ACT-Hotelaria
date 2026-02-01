using ACT_Hotelaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.SqlServer.Persistence.Configurations;

public class ConsumptionConfiguration : BaseConfiguration<Consumption>
{
    public override void Configure(EntityTypeBuilder<Consumption> builder)
    {
        base.Configure(builder);
        builder.ToTable("Consumos");

        builder.Property(c => c.QtyProduct)
            .IsRequired()
            .HasColumnName("Quantidade");

        builder.Property(c => c.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasColumnName("PrecoUnitarioPago");


   
        builder.HasOne(c => c.Product)
            .WithMany() 
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Reservation)
            .WithMany(r => r.Consumptions) 
            .HasForeignKey(c => c.ReservationId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}