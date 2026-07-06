using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Features.Organizations.Queries.ListUsersOrganizations;

public record ListUsersOrganizationsQuery() : IRequest<Result<List<OrganizationDto>>>;