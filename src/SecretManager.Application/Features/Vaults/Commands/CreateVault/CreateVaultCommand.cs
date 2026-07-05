using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Vaults.Commands.CreateVault;

public record CreateVaultCommand(string Name,Guid OwnerId, string EncryptedKey, Guid? OrganizationId)
: IRequest<Result<Guid>>;