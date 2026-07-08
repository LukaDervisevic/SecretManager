using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Mappings;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Collections.Queries.ListCollectionsByVault;

public class ListCollectionsByVaultHandler(IUnitOfWork uow, ILoggedInUserService currentService)
: IRequestHandler<ListCollectionsByVaultQuery, Result<List<CollectionDto>>>
{
    public async Task<Result<List<CollectionDto>>> Handle(ListCollectionsByVaultQuery request, CancellationToken cancellationToken)
    {
        var vault = await uow.VaultRepository.GetVault(request.VaultId, cancellationToken);
        
        if (vault is null)
            return Result.Failure<List<CollectionDto>>("Vault not found.");

        if (vault.OwnerId != currentService.UserId)
            return Result.Failure<List<CollectionDto>>("You don't have access to this vault nor collections");

        var collections = await uow.CollectionRepository.GetCollectionsByVaultIdAsync(vault.Id);
        
        return Result.Success(collections.Select(c => c.ToDto()).ToList());
    }
}