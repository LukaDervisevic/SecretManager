using SecretManager.Application.Common.Interfaces;

namespace SecretManager.Infrastructure.Jobs;

public class AuditLogCleanupJob(IAppDbContext db)
{
    public async Task Execute()
    {
        var cutoff = DateTime.UtcNow.AddDays(-90);
        var oldLogs = db.AuditLogs.Where(a => a.Timestamp < cutoff);
        db.AuditLogs.RemoveRange(oldLogs);
        await db.SaveChangesAsync(CancellationToken.None);
    }
}