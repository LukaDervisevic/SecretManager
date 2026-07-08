using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface ICollectionRepository
{
    void Add(Collection collection);
    void Remove(Collection collection);
    Task<List<Collection>> GetCollectionsByVaultIdAsync(Guid vaultId);
    Task<Collection?> GetCollectiionByIdAsync(Guid vaultId, Guid collectionId,CancellationToken cancellationToken = default);
}