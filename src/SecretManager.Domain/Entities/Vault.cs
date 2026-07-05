namespace SecretManager.Domain.Entities;

public class Vault
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }            
    public User Owner { get; private set; } = null!;

    public Guid? OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    public string EncryptedKey { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private readonly List<Collection> _collections = [];
    public IReadOnlyCollection<Collection> Collections => _collections.AsReadOnly();

    private readonly List<Secret> _secrets = [];
    public IReadOnlyCollection<Secret> Secrets => _secrets.AsReadOnly();

    private Vault() { }

    public static Vault Create(string name, Guid ownerId, string encryptedKey, Guid? organizationId = null)
    {
        return new Vault
        {
            Id = Guid.NewGuid(),
            Name = name,
            OwnerId = ownerId,
            EncryptedKey = encryptedKey,
            OrganizationId = organizationId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Rename(string newName) => Name = newName;

    public void AddCollection(Collection collection)
    {
        if (collection.VaultId != Id)
        {
            throw new InvalidOperationException("Collection doesn't belong to this vault");
        }
        _collections.Add(collection);
    }

    public void AddSecret(Secret secret)
    {
        if (secret.VaultId != Id)
        {
            throw new InvalidOperationException("Secret doesn't belong to this vault");
        }
        _secrets.Add(secret);
    }
}