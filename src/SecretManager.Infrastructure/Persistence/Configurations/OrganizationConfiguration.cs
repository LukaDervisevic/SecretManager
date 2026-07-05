using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretManager.Domain.Entities;

namespace SecretManager.Infrastructure.Persistence.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
        builder.Property(o => o.CreatedAt).IsRequired();

        builder.HasOne(o => o.Owner)
            .WithMany(u => u.OwnedOrganizations)
            .HasForeignKey(o => o.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);   

        builder.HasMany(o => o.Vaults)
            .WithOne(v => v.Organization)
            .HasForeignKey(v => v.OrganizationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(o => o.Members)
            .WithOne(m => m.Organization)
            .HasForeignKey(m => m.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}