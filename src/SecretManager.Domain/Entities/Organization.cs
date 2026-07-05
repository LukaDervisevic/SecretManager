using SecretManager.Domain.Enums;

namespace SecretManager.Domain.Entities;

public class Organization
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }
    public User Owner { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    private readonly List<Member> _members = [];
    public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

    private readonly List<Vault> _vaults = [];
    public IReadOnlyCollection<Vault> Vaults => _vaults.AsReadOnly();

    private Organization() { }

    public static Organization Create(string name, Guid ownerId)
    {
        return new Organization
        {
            Id = Guid.NewGuid(),
            Name = name,
            OwnerId = ownerId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddMember(User member,Guid organizationId ,AccessPolicy role = AccessPolicy.User)
    {
        if (organizationId != Id)
        {
            throw new InvalidOperationException("User does not belong to this organization.");
        }
        
        _members.Add(Member.Create(Id, member.Id, role));
    }

    public void RemoveMember(Guid memberId)
    {
        if (memberId == OwnerId)
        {
            throw new InvalidOperationException("Cannot remove the owner from the organiaztion");
        }
        
        
        var member = _members.FirstOrDefault(m => m.UserId == memberId);
        if (member is null)
            throw new InvalidOperationException($"User '{memberId}' is not a member of this organization");

        _members.Remove(member);
    }

    public void AddVault(Vault vault)
    {
        if (vault.OrganizationId != Id)
        {
            throw new InvalidOperationException("Vault does not belong to this organization.");
        }
        
        _vaults.Add(vault);
    }
}