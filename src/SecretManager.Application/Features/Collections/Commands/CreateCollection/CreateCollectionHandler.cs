using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Collections.Commands.CreateCollection;

public class CreateCollectionHandler(IAppDbContext db, ILoggedInUserService currentUser)
: IRequestHandler<CreateCollectionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var vault = await db.Vaults.FirstOrDefaultAsync(v => v.Id == request.VaultId, cancellationToken);
        if (vault is null)
            return Result.Failure<Guid>("Vault not found.");

        var collection = Collection.Create(request.Name, request.OwnerId,request.VaultId);
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.CollectionCreated,
            nameof(Collection), collection.Id, currentUser.IpAddress);

        db.Collections.Add(collection);
        db.AuditLogs.Add(auditLog);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Success(collection.Id);
    }
}