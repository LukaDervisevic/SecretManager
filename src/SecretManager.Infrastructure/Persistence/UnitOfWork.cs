using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Infrastructure.Repositories;

namespace SecretManager.Infrastructure.Persistence;

public class UnitOfWork(
    AppDbContext db,
    IVaultRepository vaultRepository,
    ISecretRepository secretRepository,
    ICollectionRepository collectionRepository,
    IMemberRepository memberRepository,
    IOrganizationRepository organizationRepository,
    IAuditLogRepository auditLogRepository,
    IUserRepository userRepository) :IUnitOfWork
{
    public IVaultRepository VaultRepository => vaultRepository;
    public ISecretRepository SecretRepository => secretRepository;
    public ICollectionRepository CollectionRepository => collectionRepository;
    public IMemberRepository MemberRepository => memberRepository;
    public IOrganizationRepository OrganizationRepository => organizationRepository;
    public IAuditLogRepository AuditLogRepository => auditLogRepository;
    public IUserRepository UserRepository => userRepository;
    
    
    public Task SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
}