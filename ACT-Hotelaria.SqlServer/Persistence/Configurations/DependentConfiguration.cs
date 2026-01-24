using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.Infra.Persistence.Configurations;

public class DependentConfiguration : BaseConfiguration<Dependent>
{
    public override void Configure(EntityTypeBuilder<Dependent> builder)
    {
        base.Configure(builder);
        builder.ToTable("Dependentes");
        
        builder.Property(d => d.Name)
            .HasMaxLength(255)
            .HasColumnName("Name")
            .IsRequired();

        builder.Property(d => d.CPF)
            .HasMaxLength(11)
            .HasConversion(d => d.Value, dbValue => new Cpf(dbValue))
            .IsRequired()
            .HasColumnName("CPF");

        builder.HasOne(d => d.Client)
            .WithMany(d => d.Dependents)
            .HasForeignKey(d => d.ClientId)
            .IsRequired();
    }
}