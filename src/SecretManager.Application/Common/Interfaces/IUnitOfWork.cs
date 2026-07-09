using SecretManager.Application.Common.Interfaces.Repositories;

namespace SecretManager.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IVaultRepository VaultRepository { get; }
    ISecretRepository SecretRepository { get; }
    ICollectionRepository CollectionRepository { get; }
    IMemberRepository MemberRepository { get; }
    IOrganizationRepository OrganizationRepository { get; }
    IAuditLogRepository AuditLogRepository { get; }
    IUserRepository UserRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IProizvodRepository ProizvodRepository { get; }
    
    Task SaveChangesAsync(CancellationToken ct = default);
}