namespace SecretManager.Domain.Enums;

public enum AuditAction
{
    SecretCreated,
    SecretRead,
    SecretUpdated,
    SecretDeleted,
    VaultCreated,
    VaultDeleted,
    CollectionCreated,
    CollectionDeleted,
    OrganizationCreated,
    OrganizationAddMember,
    OrganizationRemoveMember,
    UserRegistered,
    UserLoggedIn,
    UserLoggedOut,
    PasswordChanged,
}