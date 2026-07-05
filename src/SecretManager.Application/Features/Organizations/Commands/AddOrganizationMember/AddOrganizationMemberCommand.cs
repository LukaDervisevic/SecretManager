using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Organizations.Commands.AddOrganizationMember;

public record AddOrganizationMemberCommand(Guid OrganizationId, Guid UserId)
: IRequest<Result>;