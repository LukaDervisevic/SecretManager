using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface ISecretRepository
{
    void Add(Secret secret);
    void Remove(Secret secret);
    Secret Update(Secret secret);
    Task<List<Secret>> GetVaultSecrets(Guid vaultId,CancellationToken cancellationToken = default);
    Task<List<Secret>> GetCollectionSecrets(Guid vaultId, Guid collectionId,CancellationToken cancellationToken = default);
    Task<Secret?> GetSecret(Guid secretId, CancellationToken cancellationToken = default);
}