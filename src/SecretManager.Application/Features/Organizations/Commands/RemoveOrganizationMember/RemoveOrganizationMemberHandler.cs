using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Organizations.Commands.RemoveOrganizationMember;

public class RemoveOrganizationMemberCommandHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
    : IRequestHandler<RemoveOrganizationMemberCommand, Result>
{
    public async Task<Result> Handle(RemoveOrganizationMemberCommand request, CancellationToken cancellationToken)
    {
        var organization = await uow.OrganizationRepository.GetOrganization(request.OrganizationId, cancellationToken);

        if (organization is null)
            return Result.Failure("Organization not found.");

        if (organization.OwnerId != currentUser.UserId)
            return Result.Failure("Only the organization owner can remove members.");

        if (organization.OwnerId == request.UserId)
            return Result.Failure("Cannot remove the owner from the organization.");
        
        var member = Member.Create(organization.Id, request.UserId, AccessPolicy.User);
        uow.MemberRepository.Remove(member);
        
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.OrganizationRemoveMember, nameof(Organization), organization.Id, currentUser.IpAddress);
        uow.AuditLogRepository.Add(auditLog);

        await uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}