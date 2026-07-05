using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Secrets.Queries.GetSecretById;

public record SecretDto(
    Guid Id,
    string Name,
    SecretType Type,
    string CiphertextBlob,
    Guid VaultId,
    Guid? CollectionId,
    DateTime CreatedAt,
    DateTime UpdateAt
    );