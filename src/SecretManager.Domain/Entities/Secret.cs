using SecretManager.Domain.Enums;
namespace SecretManager.Domain.Entities;

public class Secret
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public SecretType Type { get; private set; }
    public string CiphertextBlob { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }            
    public User Owner { get; private set; } = null!;

    public Guid VaultId { get; private set; }
    public Vault Vault { get; private set; } = null!;

    public Guid? CollectionId { get; private set; }
    public Collection? Collection { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Secret() { }

    public static Secret Create(string name, SecretType type, string ciphertextBlob,
        Guid ownerId, Guid vaultId, Guid? collectionId = null)
    {
        return new Secret
        {
            Id = Guid.NewGuid(),
            Name = name,
            Type = type,
            CiphertextBlob = ciphertextBlob,
            OwnerId = ownerId,
            VaultId = vaultId,
            CollectionId = collectionId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateCiphertext(string newCiphertextBlob) { CiphertextBlob = newCiphertextBlob; UpdatedAt = DateTime.UtcNow; }
    public void Rename(string newName) { Name = newName; UpdatedAt = DateTime.UtcNow; }
    public void MoveToCollection(Guid? collectionId) { CollectionId = collectionId; UpdatedAt = DateTime.UtcNow; }
}