using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Secrets.Queries.ListVaultSecrets;

public record ListSecretsByVaultQuery(Guid VaultId, int PageNumber = 1, int PageSize = 25)
    : IRequest<Result<List<SecretDto>>>;