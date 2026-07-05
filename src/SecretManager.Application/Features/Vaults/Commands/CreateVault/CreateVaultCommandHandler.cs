using MediatR;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Vaults.Commands.CreateVault;

public class CreateVaultCommandHandler(IAppDbContext db, ILoggedInUserService currentUser)
: IRequestHandler<CreateVaultCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateVaultCommand request, CancellationToken cancellationToken)
    {
        var vault = Vault.Create(request.Name, currentUser.UserId, request.EncryptedKey, request.OrganizationId);
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.VaultCreated, nameof(Vault), vault.Id,
            currentUser.IpAddress);

        db.Vaults.Add(vault);
        db.AuditLogs.Add(auditLog);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success(vault.Id);
    }
}