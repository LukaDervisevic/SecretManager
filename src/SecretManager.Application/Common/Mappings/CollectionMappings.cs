using SecretManager.Application.Common.Dtos;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Mappings;

public static class CollectionMappings
{
    public static CollectionDto ToDto(this Collection collection) => new(
        collection.Id,
        collection.Name,
        collection.OwnerId,
        collection.VaultId,
        collection.CreatedAt,
        collection.Secrets.Select(s => s.ToDto()).ToList()
    );
}