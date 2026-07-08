using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class AuditLogRepository(AppDbContext db): IAuditLogRepository
{
    public void Add(AuditLog auditLog) => db.AuditLogs.Add(auditLog);
        
}