using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Organizations.Commands.AddOrganizationMember;

public class AddOrganizationMemberHandler(IAppDbContext db, ILoggedInUserService currentUser)
: IRequestHandler<AddOrganizationMemberCommand, Result>
{
    public async Task<Result> Handle(AddOrganizationMemberCommand request, CancellationToken cancellationToken)
    {
        var organzation = await db.Organizations
            .Include(o => o.Members)
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId, cancellationToken);

        if (organzation is null)
            return Result.Failure("Organization not found.");

        if (organzation.OwnerId != currentUser.UserId)
            return Result.Failure("Only the organization owner can add members");

        var userToAdd = await db.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (userToAdd is null)
            return Result.Failure("User not found.");

        if (organzation.Members.Any(m => m.Id == request.UserId))
            return Result.Failure("User is already a member of this organization");

        if (organzation.OwnerId == request.UserId)
            return Result.Failure("Owner is already part of the organization.");

        organzation.AddMember(userToAdd, request.OrganizationId);

        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.OrganizationAddMember, nameof(Organization),
            organzation.Id, currentUser.IpAddress);
        db.AuditLogs.Add(auditLog);

        await db.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}