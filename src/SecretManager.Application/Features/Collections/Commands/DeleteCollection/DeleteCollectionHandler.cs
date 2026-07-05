using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Collections.Commands.DeleteCollection;

public class DeleteCollectionHandler(IAppDbContext db, ILoggedInUserService currentUser)
: IRequestHandler<DeleteCollectionCommand,Result>
{
    public async Task<Result> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = await db.Collections.FirstOrDefaultAsync(c => c.Id == request.CollectionId, cancellationToken);
        if (collection is null)
            return Result.Failure("Collection not found.");

        db.Collections.Remove(collection);
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.CollectionDeleted, nameof(Collection), collection.Id,
            currentUser.IpAddress);
        db.AuditLogs.Add(auditLog);

        await db.SaveChangesAsync(cancellationToken);
        return Result.Success(collection.Id);
    }
}