using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Features.Vaults.Queries.ListUsersVaultsQuery;

public record ListUsersVaultsQuery : IRequest<Result<List<VaultDto>>>;
