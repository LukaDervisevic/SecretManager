using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Collections.Queries.ListCollectionsByVault;

public class ListCollectionsByVaultHandler(IAppDbContext db, ILoggedInUserService currentService)
: IRequestHandler<ListCollectionsByVaultQuery, Result<List<CollectionDto>>>
{
    public async Task<Result<List<CollectionDto>>> Handle(ListCollectionsByVaultQuery request, CancellationToken cancellationToken)
    {
        var vault = await db.Vaults.FirstOrDefaultAsync(v => v.Id == request.VaultId, cancellationToken);

        if (vault is null)
            return Result.Failure<List<CollectionDto>>("Vault not found.");

        if (vault.OwnerId != currentService.UserId)
            return Result.Failure<List<CollectionDto>>("You don't have access to this vault nor collections");

        var collections = await db.Collections
            .Include(c => c.Secrets)
            .Where(c => c.VaultId == request.VaultId)
            .OrderBy(c => c.Name)
            .Select(c => new CollectionDto(
                c.Id,
                c.Name,
                c.OwnerId,
                c.VaultId,
                c.CreatedAt,
                c.Secrets.Select(s => new SecretDto(
                    s.Id, s.Name, s.Type, s.CiphertextBlob, s.OwnerId, s.VaultId, s.CollectionId, s.CreatedAt, s.UpdatedAt
                )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(collections);
    }
}