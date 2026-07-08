using System.Security.Cryptography;
using MediatR;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Vaults.Commands.CreateVault;

public class CreateVaultCommandHandler(IUnitOfWork uow, ILoggedInUserService currentUser,IEncryptionService encryptionService)
: IRequestHandler<CreateVaultCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateVaultCommand request, CancellationToken cancellationToken)
    {
        var vaultKeyBytes = new byte[32];
        RandomNumberGenerator.Fill(vaultKeyBytes);
        var vaultKey = Convert.ToBase64String(vaultKeyBytes);

        var encryptedVaultKey = encryptionService.EncryptVaultKey(vaultKey, request.MasterKey);
        
        var vault = Vault.Create(request.Name, currentUser.UserId, encryptedVaultKey, request.OrganizationId);
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.VaultCreated, nameof(Vault), vault.Id,
            currentUser.IpAddress);
        
        uow.VaultRepository.Add(vault);
        uow.AuditLogRepository.Add(auditLog);
        
        await uow.SaveChangesAsync(cancellationToken);

        return Result.Success(vault.Id);
    }
}