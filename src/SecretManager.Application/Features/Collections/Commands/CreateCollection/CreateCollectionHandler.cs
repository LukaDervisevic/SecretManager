using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Collections.Commands.CreateCollection;

public class CreateCollectionHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<CreateCollectionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var vault = await uow.VaultRepository.GetVault(request.VaultId,cancellationToken);
        if (vault is null)
            return Result.Failure<Guid>("Vault not found.");

        var collection = Collection.Create(request.Name, request.OwnerId,request.VaultId);
        var auditLog = AuditLog.Record(currentUser.UserId, AuditAction.CollectionCreated,
            nameof(Collection), collection.Id, currentUser.IpAddress);
        
        uow.CollectionRepository.Add(collection);
        uow.AuditLogRepository.Add(auditLog);
        await uow.SaveChangesAsync(cancellationToken);
        
        return Result.Success(collection.Id);
    }
}