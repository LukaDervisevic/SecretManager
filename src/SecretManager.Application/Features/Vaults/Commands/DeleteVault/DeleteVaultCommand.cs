using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Vaults.Commands.DeleteVault;

public record DeleteVaultCommand(Guid VaultId) : IRequest<Result>;
