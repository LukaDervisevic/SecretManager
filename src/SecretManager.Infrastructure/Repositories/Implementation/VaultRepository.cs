using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class VaultRepository(AppDbContext db) : IVaultRepository
{
    public Task<List<Vault>> GetOrganizationsVaults(Guid organizationId,CancellationToken cancellationToken) =>
        db.Vaults
            .Include(v => v.Secrets)
            .Include(v => v.Collections)
            .ThenInclude(c => c.Secrets)
            .Where(v => v.OrganizationId == organizationId)
            .OrderBy(v => v.Name)
            .ToListAsync(cancellationToken);

    public Task<List<Vault>> GetUsersVaults(Guid userId,CancellationToken cancellationToken) => 
        db.Vaults
            .Include(v => v.Secrets)
            .Include(v => v.Collections)
            .ThenInclude(c => c.Secrets)
            .Where(v => v.OwnerId == userId ||
                        (v.OrganizationId != null &&
                         db.Organizations.Any(o =>
                             o.Id == v.OrganizationId &&
                             (o.OwnerId == userId || o.Members.Any(m => m.UserId == userId))
                         )
                        ))
            .OrderBy(v => v.Name)
            .ToListAsync(cancellationToken);

    public void Add(Vault vault) => db.Vaults.Add(vault);

    public void Remove(Vault vault) => db.Vaults.Remove(vault);

    public Task<Vault?> GetVault(Guid vaultId, CancellationToken cancellationToken = default) =>
        db.Vaults.FirstOrDefaultAsync(v => v.Id == vaultId, cancellationToken);
}