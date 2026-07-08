using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface IOrganizationRepository
{
    void Add(Organization organization);
    Task<List<Organization>> GetUsersOrganizations(Guid userId, CancellationToken cancellationToken = default);
    
    Task<Organization?> GetOrganization(Guid id,CancellationToken cancellationToken = default);
}