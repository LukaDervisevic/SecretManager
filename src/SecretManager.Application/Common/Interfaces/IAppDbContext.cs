using Microsoft.EntityFrameworkCore;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Vault> Vaults { get; }
    DbSet<Secret> Secrets { get; }
    DbSet<Collection> Collections { get; }
    DbSet<Organization> Organizations { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}