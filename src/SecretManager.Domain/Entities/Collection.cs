namespace SecretManager.Domain.Entities;

public class Collection
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }            
    public Guid? OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    public Guid VaultId { get; private set; }
    public Vault Vault { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    private readonly List<Secret> _secrets = [];          
    public IReadOnlyCollection<Secret> Secrets => _secrets.AsReadOnly();

    private Collection() { }

    public static Collection Create(string name, Guid ownerId, Guid vaultId, Guid? organizationId = null)
    {
        return new Collection
        {
            Id = Guid.NewGuid(),
            Name = name,
            OwnerId = ownerId,
            VaultId = vaultId,
            OrganizationId = organizationId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Rename(string newName) => Name = newName;

    public void AddSecret(Secret secret)
    {
        if (secret.CollectionId != Id)
        {
            throw new InvalidOperationException("Secret doesn't belong to this collection");
        }
        _secrets.Add(secret);
    }
}