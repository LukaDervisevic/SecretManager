using SecretManager.Domain.Enums;

namespace SecretManager.Application.Common.Dtos;

public record SecretDto(
    Guid Id,
    string Name,
    SecretType Type,
    string CiphertextBlob,
    Guid OwnerId,
    Guid VaultId,
    Guid? CollectionId,
    DateTime CreatedAt,
    DateTime UpdatedAt);