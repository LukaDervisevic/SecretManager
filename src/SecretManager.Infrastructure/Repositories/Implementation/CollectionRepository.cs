using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class CollectionRepository(AppDbContext db): ICollectionRepository
{
    public void Add(Collection collection) => db.Collections.Add(collection);

    public void Remove(Collection collection) => db.Collections.Remove(collection);

    public Task<List<Collection>> GetCollectionsByVaultIdAsync(Guid vaultId) =>
        db.Collections
            .Include(c => c.Secrets)
            .Where((Collection c)  => c.VaultId == vaultId)
            .OrderBy((Collection c) => c.Name)
            .ToListAsync();

    public Task<Collection?> GetCollectiionByIdAsync(Guid vaultId, Guid collectionId, CancellationToken cancellationToken = default) => 
        db.Collections.FirstOrDefaultAsync(c => c.VaultId == vaultId && c.Id == collectionId, cancellationToken);
}