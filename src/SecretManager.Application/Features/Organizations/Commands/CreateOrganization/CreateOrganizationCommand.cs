using MediatR;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Features.Organizations.Commands.CreateOrganization;

public record CreateOrganizationCommand(
    string Name,
    Guid OwnerId): IRequest<Result<Guid>>;