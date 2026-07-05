using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Organizations.Commands.RemoveOrganizationMember;

public class RemoveOrganizationMemberCommandHandler(IAppDbContext db, ILoggedInUserService currentUser)
    : IRequestHandler<RemoveOrganizationMemberCommand, Result>
{
    public async Task<Result> Handle(RemoveOrganizationMemberCommand request, CancellationToken cancellationToken)
    {
        var organization = await db.Organizations
            .Include(o => o.Members)
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId, cancellationToken);

        if (organization is null)
            return Result.Failure("Organization not found.");

        if (organization.OwnerId != currentUser.UserId)
            return Result.Failure("Only the organization owner can remove members.");

        if (organization.OwnerId == request.UserId)
            return Result.Failure("Cannot remove the owner from the organization.");

        organization.RemoveMember(request.UserId);

        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.OrganizationRemoveMember, nameof(Organization), organization.Id, currentUser.IpAddress);
        db.AuditLogs.Add(auditLog);

        await db.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}