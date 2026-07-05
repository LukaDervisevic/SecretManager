// File: .../Configurations/SecretConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretManager.Domain.Entities;

namespace SecretManager.Infrastructure.Persistence.Configurations;

public class SecretConfiguration : IEntityTypeConfiguration<Secret>
{
    public void Configure(EntityTypeBuilder<Secret> builder)
    {
        builder.ToTable("Secrets");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
        builder.Property(s => s.CiphertextBlob).IsRequired();
        builder.Property(s => s.Type).HasConversion<string>().HasMaxLength(50).IsRequired();
        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.UpdatedAt).IsRequired();

        builder.HasOne(s => s.Owner)
            .WithMany(u => u.Secrets)
            .HasForeignKey(s => s.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}