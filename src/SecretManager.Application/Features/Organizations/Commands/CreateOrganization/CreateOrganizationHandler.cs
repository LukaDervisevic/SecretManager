using MediatR;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationHandler(IUnitOfWork uow, ILoggedInUserService currentUser)
: IRequestHandler<CreateOrganizationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = Organization.Create(request.Name, request.OwnerId);
        var auditLog = AuditLog.Record(currentUser.UserId,
            AuditAction.OrganizationCreated, nameof(Organization),organization.Id, currentUser.IpAddress);
        
        uow.OrganizationRepository.Add(organization);
        uow.AuditLogRepository.Add(auditLog);

        await uow.SaveChangesAsync(cancellationToken);
        
        return Result.Success(organization.Id);
    }
}