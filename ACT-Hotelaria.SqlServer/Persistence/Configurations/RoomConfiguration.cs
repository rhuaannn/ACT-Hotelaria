using ACT_Hotelaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.SqlServer.Persistence.Configurations;

public class RoomConfiguration : BaseConfiguration<Room>
{
    public override void Configure(EntityTypeBuilder<Room> builder)
    {
        base.Configure(builder);
        builder.ToTable("Quartos");

        builder.Property(r => r.Type)
            .HasConversion<string>()
            .HasColumnName("Tipo")
            .IsRequired();

        builder.Property(r => r.QtyRoom)
            .HasColumnName("Quantidade")
            .IsRequired();
        
        builder.HasMany(r => r.Reservations)
            .WithOne(rm => rm.Room)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
      
    }
}