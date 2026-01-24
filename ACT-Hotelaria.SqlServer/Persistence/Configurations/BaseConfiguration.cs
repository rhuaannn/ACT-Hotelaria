using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.Infra.Persistence.Configurations;

public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(e => e.Id)
            .IsRequired();
        
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2");

        builder.Property(c => c.Active)
            .IsRequired()
            .HasDefaultValue(true);
    }
}

  