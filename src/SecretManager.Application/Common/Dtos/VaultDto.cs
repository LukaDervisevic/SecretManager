namespace SecretManager.Application.Common.Dtos;

public record VaultDto(
    Guid Id,
    string Name,
    Guid OwnerId,
    Guid? OrganizationId,
    string EncryptedKey,
    DateTime CreatedAt,
    List<CollectionDto> Collections,
    List<SecretDto> Secrets
    );
    