using SecretManager.Application.Common.Dtos;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Mappings;

public static class VaultMappings
{
    public static VaultDto ToDto(this Vault vault) => new(
        vault.Id,
        vault.Name,
        vault.OwnerId,
        vault.OrganizationId,
        vault.EncryptedKey,
        vault.CreatedAt,
        vault.Collections.Select(c => c.ToDto()).ToList(),
        vault.Secrets.Select(s => s.ToDto()).ToList()
    );
}