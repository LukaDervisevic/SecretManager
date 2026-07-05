using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Features.Vaults.Queries.ListOrganizationsVaults;

public record ListOrganizationsVaultsQuery(Guid OrganizationId) : IRequest<Result<List<VaultDto>>>;