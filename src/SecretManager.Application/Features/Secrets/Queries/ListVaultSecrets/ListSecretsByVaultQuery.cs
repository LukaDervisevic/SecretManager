using MediatR;
using SecretManager.Application.Common.Models;
using SecretManager.Application.Features.Secrets.Queries.GetSecretById;

namespace SecretManager.Application.Features.Secrets.Queries;

public record ListSecretsByVaultQuery(Guid VaultId, int PageNumber = 1, int PageSize = 25)
    : IRequest<Result<PaginatedList<SecretDto>>>;