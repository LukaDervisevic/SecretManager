using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class OrganizationRepository(AppDbContext db): IOrganizationRepository
{
    public void Add(Organization organization) => db.Add(organization);

    public Task<List<Organization>> GetUsersOrganizations(Guid userId,CancellationToken cancellationToken = default) => 
        db.Organizations
            .Include(o => o.Members)
            .Include(o => o.Vaults)
                .ThenInclude(v => v.Secrets)
            .Include(o => o.Vaults)
                .ThenInclude(v => v.Collections)
                .ThenInclude(c => c.Secrets)
            .Where(o => o.OwnerId == userId || o.Members.Any(m => m.UserId == userId))
            .OrderBy(o => o.Name)
            .ToListAsync(cancellationToken);

    public Task<Organization?> GetOrganization(Guid id,CancellationToken cancellationToken = default) =>
        db.Organizations.FirstOrDefaultAsync(o => o.Id == id,cancellationToken);
}