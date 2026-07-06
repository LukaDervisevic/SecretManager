using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Organizations.Queries.ListUsersOrganizations;

public class ListUsersOrganizationsHandler(IAppDbContext db, ILoggedInUserService currentUser)
: IRequestHandler<ListUsersOrganizationsQuery, Result<List<OrganizationDto>>>
{
    public async Task<Result<List<OrganizationDto>>> Handle(ListUsersOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;

        var organizations = await db.Organizations
            .Include(o => o.Members)
            .Include(o => o.Vaults)
            .ThenInclude(v => v.Secrets)
            .Include(o => o.Vaults)
            .ThenInclude(v => v.Collections)
            .ThenInclude(c => c.Secrets)
            .Where(o => o.OwnerId == userId || o.Members.Any(m => m.UserId == userId))
            .OrderBy(o => o.Name)
            .Select(o => new OrganizationDto(
                o.Id,
                o.Name,
                o.OwnerId,
                o.CreatedAt,
                o.Members.Select(m => new MemberDto(
                    m.Id,
                    m.OrganizationId,
                    m.UserId,
                    m.Role,
                    m.JoinedAt
                )).ToList(),
                o.Vaults.Select(v => new VaultDto(
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
                        c.VaultId,
                        c.CreatedAt,
                        c.Secrets.Select(s => new SecretDto(
                            s.Id, s.Name, s.Type, s.CiphertextBlob, s.OwnerId, s.VaultId, s.CollectionId, s.CreatedAt, s.UpdatedAt
                        )).ToList()
                    )).ToList(),
                    v.Secrets.Select(s => new SecretDto(
                        s.Id, s.Name, s.Type, s.CiphertextBlob, s.OwnerId, s.VaultId, s.CollectionId, s.CreatedAt, s.UpdatedAt
                    )).ToList()
                )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(organizations);
    }
}