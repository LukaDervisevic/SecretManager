using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Features.Vaults.Queries.ListUsersVaultsQuery;

public class ListUsersVaultsHandler(IAppDbContext db, ILoggedInUserService currentUser)
: IRequestHandler<ListUsersVaultsQuery,Result<List<VaultDto>>>
{
    public async Task<Result<List<VaultDto>>> Handle(ListUsersVaultsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;

        var vaults = await db.Vaults
            .Include(v => v.Secrets)
            .Include(v => v.Collections)
            .ThenInclude(c => c.Secrets)
            .Where(v => v.OwnerId == userId)
            .OrderBy(v => v.Name)
            .Select(v => new VaultDto(
                v.Id,
                v.Name,
                v.OwnerId,
                v.OrganizationId,
                v.EncryptedKey,
                v.CreatedAt,
                v.Collections.Select(c => new CollectionDto(
                    c.Id,
                    c.Name,
                    c.OwnerId,
                    c.OrganizationId,
                    c.VaultId,
                    c.CreatedAt,
                    c.Secrets.Select(s => new SecretDto(
                        s.Id, s.Name, s.Type, s.CiphertextBlob, s.OwnerId, s.VaultId, s.CollectionId, s.CreatedAt, s.UpdatedAt
                    )).ToList()
                )).ToList(),
                v.Secrets.Select(s => new SecretDto(
                    s.Id, s.Name, s.Type, s.CiphertextBlob, s.OwnerId, s.VaultId, s.CollectionId, s.CreatedAt, s.UpdatedAt
                )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(vaults);
    }
}