using MediatR;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Secrets.Commands.CreateSecret;

public record CreateSecretCommand(
    string Name,
    SecretType Type,
    string Ciphertextblob,
    Guid OwnerId,
    Guid VaultId,
    Guid? CollectionId
    ): IRequest<Result<Guid>>;