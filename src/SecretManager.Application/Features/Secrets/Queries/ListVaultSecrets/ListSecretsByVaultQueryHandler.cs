using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Application.Features.Secrets.Queries.GetSecretById;

namespace SecretManager.Application.Features.Secrets.Queries.ListVaultSecrets;

public class ListSecretsByVaultQueryHandler(IAppDbContext db, ILoggedInUserService currentUser)
    : IRequestHandler<ListSecretsByVaultQuery, Result<PaginatedList<SecretDto>>>
{
    public async Task<Result<PaginatedList<SecretDto>>> Handle(ListSecretsByVaultQuery request, CancellationToken cancellationToken)
    {
        var vault = await db.Vaults.FirstOrDefaultAsync(v => v.Id == request.VaultId, cancellationToken);

        if (vault is null)
            return Result.Failure<PaginatedList<SecretDto>>("Vault not found");

        if (vault.OwnerId != currentUser.UserId)
            return Result.Failure<PaginatedList<SecretDto>>("You do not have access to this vault.");

        var query = db.Secrets
            .Where(s => s.VaultId == request.VaultId)
            .OrderBy(s => s.Name)
            .Select(s => new SecretDto(
                s.Id,
                s.Name,
                s.Type,
                s.CiphertextBlob,
                s.VaultId,
                s.CollectionId,
                s.CreatedAt,
                s.UpdatedAt));

        var result =
            await PaginatedList<SecretDto>.CreateAsync(query, request.PageNumber, request.PageSize, cancellationToken);
        return Result.Success(result);
    }
}