using ACT_Hotelaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.SqlServer.Persistence.Configurations;

public class InvoicingConfiguration : BaseConfiguration<Invoicing>
{
    public override void Configure(EntityTypeBuilder<Invoicing> builder)
    {
        base.Configure(builder);
        builder.ToTable("Faturamentos");

        builder.Property(i => i.IssueDate)
            .IsRequired()
            .HasColumnName("DataEmissao");

        builder.Property(i => i.TotalRoomValue)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasColumnName("TotalDiarias");

        builder.Property(i => i.TotalConsumptionValue)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasColumnName("TotalConsumo");

        builder.Property(i => i.ValueTotal)
            .HasPrecision(18, 2)
            .IsRequired()
            .HasColumnName("ValorTotalNota");

        builder.HasOne<Reservation>() 
            .WithMany()      
            .HasForeignKey(i => i.ReservationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}