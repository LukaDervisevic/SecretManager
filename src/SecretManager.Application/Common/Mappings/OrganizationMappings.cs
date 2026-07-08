using SecretManager.Application.Common.Dtos;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Mappings;

public static class OrganizationMappings
{
    public static OrganizationDto ToDto(this Organization organization) => new(
        organization.Id,
        organization.Name,
        organization.OwnerId,
        organization.CreatedAt,
        organization.Members.Select(m => m.ToDto()).ToList(),
        organization.Vaults.Select(v => v.ToDto()).ToList()
    );
}