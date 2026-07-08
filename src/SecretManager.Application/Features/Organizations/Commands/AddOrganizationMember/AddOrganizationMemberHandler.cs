using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Organizations.Commands.AddOrganizationMember;

public class AddOrganizationMemberHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<AddOrganizationMemberCommand, Result>
{
    public async Task<Result> Handle(AddOrganizationMemberCommand request, CancellationToken cancellationToken)
    {
        var organization = await uow.OrganizationRepository.GetOrganization(request.OrganizationId,cancellationToken);

        if (organization is null)
            return Result.Failure("Organization not found.");

        if (organization.OwnerId != currentUser.UserId)
            return Result.Failure("Only the organization owner can add members");
        
        var userToAdd = await uow.UserRepository.FindByIdAsync(request.UserId, cancellationToken);

        if (userToAdd is null)
            return Result.Failure("User not found.");

        if (organization.Members.Any(m => m.Id == request.UserId))
            return Result.Failure("User is already a member of this organization");

        if (organization.OwnerId == request.UserId)
            return Result.Failure("Owner is already part of the organization.");
                
        var member = Member.Create(organization.Id, request.UserId,AccessPolicy.User);
        uow.MemberRepository.Add(member);
        
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.OrganizationAddMember, nameof(Organization),
            organization.Id, currentUser.IpAddress);
        uow.AuditLogRepository.Add(auditLog);

        await uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}