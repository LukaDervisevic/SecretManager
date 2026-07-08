using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class SecretRepository(AppDbContext db) :ISecretRepository
{
    public void Add(Secret secret)
    {
        db.Add(secret);
    }

    public void Remove(Secret secret)
    {
        db.Remove(secret);
    }

    public Secret Update(Secret secret)
    {
        return db.Update(secret).Entity;
    }

    public Task<List<Secret>> GetVaultSecrets(Guid vaultId,CancellationToken cancellationToken) => 
        db.Secrets
            .Where(s => s.VaultId == vaultId && s.CollectionId == null)
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);

    public Task<List<Secret>> GetCollectionSecrets(Guid vaultId, Guid collectionId, CancellationToken cancellationToken) =>
        db.Secrets
            .Where(s => s.VaultId == vaultId && s.CollectionId == collectionId)
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);

    public Task<Secret?> GetSecret(Guid secretId, CancellationToken cancellationToken = default) => 
        db.Secrets
            .Include(s => s.Vault)
            .FirstOrDefaultAsync(s => s.Id == secretId, cancellationToken);
}