using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.SqlServer.Persistence.Configurations;

public class ReservationConfiguration : BaseConfiguration<Reservation>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);
        builder.ToTable("Reservas");
        
        builder.Property(r => r.Type)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(r => r.Checkin)
            .IsRequired()
            .HasColumnName("Checkin");
        
        builder.Property(r => r.Checkout)
            .HasColumnName("Checkout")
            .IsRequired();
        
        builder.HasOne(r => r.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder.HasMany(r => r.Consumptions)
            .WithOne(c => c.Reservation)
            .HasForeignKey(c => c.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}