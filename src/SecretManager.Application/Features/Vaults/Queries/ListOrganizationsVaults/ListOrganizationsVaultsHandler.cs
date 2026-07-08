using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Mappings;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Vaults.Queries.ListOrganizationsVaults;

public class ListOrganizationsVaultsHandler(IUnitOfWork uow)
: IRequestHandler<ListOrganizationsVaultsQuery, Result<List<VaultDto>>>
{
    public async Task<Result<List<VaultDto>>> Handle(ListOrganizationsVaultsQuery request, CancellationToken cancellationToken)
    {
        var organization  = await uow.OrganizationRepository.GetOrganization(request.OrganizationId,cancellationToken);

        if (organization is null)
            return Result.Failure<List<VaultDto>>("Organization not found.");

        var vaults = await uow.VaultRepository.GetOrganizationsVaults(request.OrganizationId, cancellationToken);
        return Result.Success(vaults.Select(v => v.ToDto()).ToList());
    }
}