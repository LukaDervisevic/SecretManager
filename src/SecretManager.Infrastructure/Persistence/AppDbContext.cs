using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Domain.Entities;

namespace SecretManager.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Vault> Vaults => Set<Vault>();
    public DbSet<Secret> Secrets => Set<Secret>();
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Proizvod> Proizvodi => Set<Proizvod>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}