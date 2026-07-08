using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface IAuditLogRepository
{
    void Add(AuditLog auditLog);
}