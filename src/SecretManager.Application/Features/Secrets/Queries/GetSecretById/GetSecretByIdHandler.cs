using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Secrets.Queries.GetSecretById;

public class GetSecretByIdHandler(IAppDbContext db, ILoggedInUserService currentUser)
    : IRequestHandler<GetSecretById, Result<SecretDto>>
{
    public async Task<Result<SecretDto>> Handle(GetSecretById request, CancellationToken cancellationToken)
    {
        var secret = await db.Secrets
            .Include(s => s.Vault)
            .FirstOrDefaultAsync(s => s.Id == request.SecretId, cancellationToken);

        if (secret is null)
            return Result.Failure<SecretDto>("Secret not found");

        if (secret.Vault is null)
            return Result.Failure<SecretDto>("Secrets vault not found");

        if (secret.Vault.OwnerId != currentUser.UserId)
            return Result.Failure<SecretDto>("You do not have access to this secret.");

        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.SecretRead, nameof(Secret), secret.Id,
            currentUser.IpAddress);
        db.AuditLogs.Add(auditLog);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success(new SecretDto(
            secret.Id,
            secret.Name,
            secret.Type,
            secret.CiphertextBlob,
            secret.VaultId,
            secret.CollectionId,
            secret.CreatedAt,
            secret.UpdatedAt));
    }
}