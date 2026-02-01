using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.SqlServer.Persistence.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder.ToTable("Produtos");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200) 
            .HasColumnName("Nome");

        builder.Property(p => p.QtyProduct)
            .IsRequired()
            .HasColumnName("QuantidadeEstoque");

        builder.Property(p => p.ValueProduct)
            .HasConversion(
                p => p.Value,     
                value => Price.Create(value) 
            )
            .HasColumnName("PrecoAtual")
            .HasPrecision(18, 2)
            .IsRequired();
    }
}