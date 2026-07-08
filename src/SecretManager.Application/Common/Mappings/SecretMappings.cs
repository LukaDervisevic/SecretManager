using SecretManager.Application.Common.Dtos;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Mappings;

public static class SecretMappings
{   
    public static SecretDto ToDto(this Secret secret) => new(
        secret.Id,
        secret.Name,
        secret.Type,
        secret.CiphertextBlob,
        secret.OwnerId,
        secret.VaultId,
        secret.CollectionId,
        secret.CreatedAt,
        secret.UpdatedAt
    );
}