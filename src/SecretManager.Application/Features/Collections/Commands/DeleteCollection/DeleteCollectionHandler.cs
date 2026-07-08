using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Collections.Commands.DeleteCollection;

public class DeleteCollectionHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<DeleteCollectionCommand,Result>
{
    public async Task<Result> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = await uow.CollectionRepository.GetCollectiionByIdAsync(request.VaultId, request.CollectionId, cancellationToken);
        if (collection is null)
            return Result.Failure("Collection not found.");
        
        uow.CollectionRepository.Remove(collection);
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.CollectionDeleted, nameof(Collection), collection.Id,
            currentUser.IpAddress);
        
        uow.AuditLogRepository.Add(auditLog);

        await uow.SaveChangesAsync(cancellationToken);
        return Result.Success(collection.Id);
    }
}