using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACT_Hotelaria.Infra.Persistence.Configurations;

public class ClientConfiguration : BaseConfiguration<Client>
{
    public override void Configure(EntityTypeBuilder<Client> builder)
    {
        base.Configure(builder);
        builder.ToTable("Clientes");
        
        builder.Property(c => c.Name)
            .HasMaxLength(255)
            .HasColumnName("Name")
            .IsRequired();
        
        builder.Property(c => c.CPF)
            .HasMaxLength(11)
            .HasConversion(
                cpf => cpf.Value,   
                dbValue => new Cpf(dbValue) 
            )            
            .HasColumnName("CPF")
            .IsRequired();
        
        builder.Property(c => c.Email)
            .HasMaxLength(255)
            .HasColumnName("Email")
            .HasConversion(
                email => email.Value, 
                dbValue => new Email(dbValue)
                )
            .IsRequired();
        
        builder.Property(c => c.Telefone)
            .HasMaxLength(11)
            .HasConversion(
                telefone => telefone.Value, 
                dbValue => new Telefone(dbValue)
                )
            .HasColumnName("Telefone")
            .IsRequired();
        
    }
}