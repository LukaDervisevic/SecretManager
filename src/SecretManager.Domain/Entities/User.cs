namespace SecretManager.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string PublicKey { get; private set; } = string.Empty;
    public string EncryptedPrivateKey { get; private set; } = string.Empty;
    public string MasterPasswordSalt { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public Guid? OrganizationId { get; private set; }
    public Organization? Organization { get; private set; }

    private readonly List<Organization> _ownedOrganizations = [];
    public IReadOnlyCollection<Organization> OwnedOrganizations => _ownedOrganizations.AsReadOnly();

    private readonly List<Vault> _vaults = [];
    public IReadOnlyCollection<Vault> Vaults => _vaults.AsReadOnly();

    private readonly List<Secret> _secrets = [];
    public IReadOnlyCollection<Secret> Secrets => _secrets.AsReadOnly();

    private User() { }

    public static User Create(string email, string passwordHash, string publicKey, string encPrivateKey, string masterPasswordSalt)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            PublicKey = publicKey,
            EncryptedPrivateKey = encPrivateKey,
            MasterPasswordSalt = masterPasswordSalt,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdatePasswordHash(string newHash) => PasswordHash = newHash;
    public void JoinOrganization(Guid organizationId) => OrganizationId = organizationId;
    public void LeaveOrganization() => OrganizationId = null;

    public void AddOrganization(Organization organization)
    {
        if (organization.OwnerId != Id)
        {
            throw new InvalidOperationException("Organization doesn't belong to this user");
        }
        _ownedOrganizations.Add(organization);
    }

    public void AddVault(Vault vault)
    {
        if (vault.OwnerId != Id)
        {
            throw new InvalidOperationException("Vault doesn't belong to this user");
        }
        _vaults.Add(vault);        
    }

    public void AddSecret(Secret secret)
    {
        if (secret.OwnerId != Id)
        {
            throw new InvalidOperationException("Secret doesn't belong to this user");
        }
        _secrets.Add(secret);
    }
}