using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Features.Organizations.Queries.ListUsersOrganizationsQuery;

public record ListUsersOrganizationsCommand() : IRequest<Result<List<OrganizationDto>>>;