using System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Auth.Commands.Register;

public class RegisterCommandHandler(
    IAppDbContext db,
    IPasswordHasher passwordHasher,
    IEncryptionService encryptionService
    ) : IRequestHandler<RegisterCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var exists = await db.Users.AnyAsync(u => u.Email == request.Email.ToLowerInvariant().Trim(), cancellationToken);
        if (exists)
            return Result.Failure<Guid>("A user with this email already exists.");

        var passwordHash = passwordHasher.Hash(request.Password);

        var keyPair = encryptionService.GenerateKeyPair(request.Password);

        var vaultKeyBytes = new byte[32];
        RandomNumberGenerator.Fill(vaultKeyBytes);
        var vaultKey = Convert.ToBase64String(vaultKeyBytes);

        var encryptedVaultKey = encryptionService.EncryptVaultKey(vaultKey, keyPair.PasswordDerivedKey);

        var user = User.Create(
            request.Email,
            passwordHash,
            keyPair.PublicKey,
            keyPair.EncryptedPrivateKey,
            keyPair.Salt);

        var vault = Vault.Create("My Vault", user.Id, encryptedVaultKey);

        var auditLogRegister = AuditLog.Record(user.Id, AuditAction.UserRegistered, nameof(User), user.Id);
        var auditLogVaultRegister = AuditLog.Record(user.Id, AuditAction.VaultCreated, nameof(Vault), vault.Id);

        db.Users.Add(user);
        db.Vaults.Add(vault);
        db.AuditLogs.Add(auditLogRegister);
        db.AuditLogs.Add(auditLogVaultRegister);

        await db.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}
