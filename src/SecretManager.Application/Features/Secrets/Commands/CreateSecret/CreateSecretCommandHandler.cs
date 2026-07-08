using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Secrets.Commands.CreateSecret;

public class CreateSecretCommandHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<CreateSecretCommand,Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateSecretCommand request, CancellationToken cancellationToken)
    {
        var vault = await uow.VaultRepository.GetVault(request.VaultId, cancellationToken);
        if (vault is null)
            return Result.Failure<Guid>("Vault does not exist");

        if (vault.OwnerId != currentUser.UserId)
            return Result.Failure<Guid>("You do not have access to this vault.");

        var secret = Secret.Create(request.Name, request.Type, request.Ciphertextblob,request.OwnerId,request.VaultId,
            request.CollectionId);
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.SecretCreated, nameof(Secret), secret.Id,
            currentUser.IpAddress);
        
        uow.SecretRepository.Add(secret);
        uow.AuditLogRepository.Add(auditLog);
        await uow.SaveChangesAsync(cancellationToken);

        return Result.Success(secret.Id);
    }
}