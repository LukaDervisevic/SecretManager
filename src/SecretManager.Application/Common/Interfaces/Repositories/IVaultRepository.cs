using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface IVaultRepository
{
    Task<List<Vault>> GetOrganizationsVaults(Guid organizationId,CancellationToken cancellationToken  = default);
    Task<List<Vault>> GetUsersVaults(Guid userId, CancellationToken cancellationToken = default);
    void Add(Vault vault);
    void Remove(Vault vault);
    
    Task<Vault?> GetVault(Guid vaultId, CancellationToken cancellationToken = default);
}