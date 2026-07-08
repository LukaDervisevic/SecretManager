using MediatR;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Vaults.Commands.DeleteVault;

public class DeleteVaultCommandHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<DeleteVaultCommand, Result>
{
    public async Task<Result> Handle(DeleteVaultCommand request, CancellationToken cancellationToken)
    {
        var vault = await uow.VaultRepository.GetVault(request.VaultId, cancellationToken);

        if (vault is null)
            return Result.Failure("Vault not found");

        if (vault.OwnerId != currentUser.UserId)
            return Result.Failure("You do not have access to this vault");
        
        uow.VaultRepository.Remove(vault);

        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.VaultDeleted, nameof(Vault), vault.Id,
            currentUser.IpAddress);
        uow.AuditLogRepository.Add(auditLog);

        await uow.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}