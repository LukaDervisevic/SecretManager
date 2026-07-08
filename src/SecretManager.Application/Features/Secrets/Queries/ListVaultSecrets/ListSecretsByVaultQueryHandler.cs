using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Mappings;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Secrets.Queries.ListVaultSecrets;

public class ListSecretsByVaultQueryHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
    : IRequestHandler<ListSecretsByVaultQuery, Result<List<SecretDto>>>
{
    public async Task<Result<List<SecretDto>>> Handle(ListSecretsByVaultQuery request, CancellationToken cancellationToken)
    {
        var vault = await uow.VaultRepository.GetVault(request.VaultId);

        if (vault is null)
            return Result.Failure<List<SecretDto>>("Vault not found");

        if (vault.OwnerId != currentUser.UserId)
            return Result.Failure<List<SecretDto>>("You do not have access to this vault.");

        var secrets = await uow.SecretRepository.GetVaultSecrets(request.VaultId, cancellationToken);
        
        return Result.Success(secrets.Select(s => s.ToDto()).ToList());
    }
}