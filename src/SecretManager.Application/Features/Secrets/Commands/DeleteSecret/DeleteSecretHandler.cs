using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Secrets.Commands.DeleteSecret;

public class DeleteSecretHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<DeleteSecretCommand,Result>
{
    public async Task<Result> Handle(DeleteSecretCommand request, CancellationToken cancellationToken)
    {
        var secret = await uow.SecretRepository.GetSecret(request.SecretId,cancellationToken);

        if (secret is null)
            return Result.Failure("Secret not found");

        if (secret.Vault is null)
            return Result.Failure("Secrets vault is not found");

        if (secret.Vault.OwnerId != currentUser.UserId)
            return Result.Failure("You do not have access to this secret");

        uow.SecretRepository.Remove(secret);
        
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.SecretDeleted, nameof(Secret), secret.Id,
            currentUser.IpAddress);
        uow.AuditLogRepository.Add(auditLog);
        await uow.SaveChangesAsync(cancellationToken);
        return Result.Success(secret.Id);
    }
}