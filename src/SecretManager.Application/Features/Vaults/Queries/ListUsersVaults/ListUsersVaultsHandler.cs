using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Mappings;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Vaults.Queries.ListUsersVaults;

public class ListUsersVaultsHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<ListUsersVaultsQuery,Result<List<VaultDto>>>
{
    public async Task<Result<List<VaultDto>>> Handle(ListUsersVaultsQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;
        
        var vaults = await uow.VaultRepository.GetUsersVaults(userId, cancellationToken);
        
        return Result.Success(vaults.Select(v => v.ToDto()).ToList());
    }
}