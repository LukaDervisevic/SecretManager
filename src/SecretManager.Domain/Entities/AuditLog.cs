using SecretManager.Domain.Enums;

namespace SecretManager.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; private set; }
    public Guid ActorId { get; private set; }
    public AuditAction Action { get; private set; }
    public string ResourceType { get; private set; } = string.Empty;
    public Guid ResourceId { get; private set; }
    public string? IpAddress { get; private set; }
    public DateTime Timestamp { get; private set; }
    
    private AuditLog() {}

    public static AuditLog Record(Guid actorId, AuditAction action, string resourceType, Guid resourceId,
        string? ipAddress = null)
    {
        return new AuditLog
        {
            Id = Guid.NewGuid(),
            ActorId = actorId,
            Action = action,
            ResourceType = resourceType,
            ResourceId = resourceId,
            IpAddress = ipAddress,
            Timestamp = DateTime.UtcNow
        };
    }
    
}