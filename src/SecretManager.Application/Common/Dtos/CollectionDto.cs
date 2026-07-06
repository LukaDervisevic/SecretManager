namespace SecretManager.Application.Common.Dtos;

public record CollectionDto(
    Guid Id,
    string Name,
    Guid OwnerId,
    Guid VaultId,
    DateTime CreatedAt,
    List<SecretDto> Secrets
    );