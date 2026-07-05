using SecretManager.Domain.Enums;

namespace SecretManager.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public AccessPolicy Role { get; private set; }
    public DateTime JoinedAt { get; private set; }

    private Member() { }

    public static Member Create(Guid organizationId, Guid userId, AccessPolicy role)
    {
        return new Member
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            UserId = userId,
            Role = role,
            JoinedAt = DateTime.UtcNow
        };
    }

    public void ChangeRole(AccessPolicy newRole) => Role = newRole;
}