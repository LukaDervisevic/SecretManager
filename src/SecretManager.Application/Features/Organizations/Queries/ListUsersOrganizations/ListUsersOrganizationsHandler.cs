using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Mappings;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Organizations.Queries.ListUsersOrganizations;

public class ListUsersOrganizationsHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<ListUsersOrganizationsQuery, Result<List<OrganizationDto>>>
{
    public async Task<Result<List<OrganizationDto>>> Handle(ListUsersOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;
        
        var organizations = await uow.OrganizationRepository.GetUsersOrganizations(currentUser.UserId, cancellationToken);

        return Result.Success(organizations.Select(o => o.ToDto()).ToList());
    }
}