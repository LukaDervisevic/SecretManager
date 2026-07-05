using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretManager.Domain.Entities;

namespace SecretManager.Infrastructure.Persistence.Configurations;

public class VaultConfiguration : IEntityTypeConfiguration<Vault>
{
    public void Configure(EntityTypeBuilder<Vault> builder)
    {
        builder.ToTable("Vaults");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name).IsRequired().HasMaxLength(200);
        builder.Property(v => v.EncryptedKey).IsRequired();
        builder.Property(v => v.CreatedAt).IsRequired();

        builder.HasOne(v => v.Owner)
            .WithMany(u => u.Vaults)
            .HasForeignKey(v => v.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.Collections)
            .WithOne(c => c.Vault)
            .HasForeignKey(c => c.VaultId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Secrets)
            .WithOne(s => s.Vault)
            .HasForeignKey(s => s.VaultId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}