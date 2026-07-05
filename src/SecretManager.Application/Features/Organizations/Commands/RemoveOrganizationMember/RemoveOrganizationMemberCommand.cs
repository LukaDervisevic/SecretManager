using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Organizations.Commands.RemoveOrganizationMember;

public record RemoveOrganizationMemberCommand(Guid OrganizationId, Guid UserId)
: IRequest<Result>;
