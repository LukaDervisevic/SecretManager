using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Vaults.Commands.DeleteVault;

public class DeleteVaultCommandHandler(IAppDbContext db, ILoggedInUserService currentUser)
: IRequestHandler<DeleteVaultCommand, Result>
{
    public async Task<Result> Handle(DeleteVaultCommand request, CancellationToken cancellationToken)
    {
        var vault = await db.Vaults.FirstOrDefaultAsync(v => v.Id == request.VaultId, cancellationToken);

        if (vault is null)
            return Result.Failure("Vault not found");

        if (vault.OwnerId != currentUser.UserId)
            return Result.Failure("You do not have access to this vault");

        db.Vaults.Remove(vault);

        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.VaultDeleted, nameof(Vault), vault.Id,
            currentUser.IpAddress);
        db.AuditLogs.Add(auditLog);

        await db.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}