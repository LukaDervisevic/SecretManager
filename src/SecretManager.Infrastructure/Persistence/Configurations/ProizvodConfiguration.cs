using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretManager.Domain.Entities;

namespace SecretManager.Infrastructure.Persistence.Configurations;

public class ProizvodConfiguration: IEntityTypeConfiguration<Proizvod>
{
    public void Configure(EntityTypeBuilder<Proizvod> builder)
    {
        builder.ToTable("Proizvodi");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Naziv).IsRequired();
        builder.Property(p => p.Cena).IsRequired();
    }
}