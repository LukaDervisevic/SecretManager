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
    IEncryptionService encryptionService,
    ITokenService tokenService
    ) : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var exists =
            await db.Users.AnyAsync(u => u.Email == request.Email.ToLowerInvariant().Trim(), cancellationToken);
        if (exists)
            return Result.Failure<AuthResponse>("A user with this email already exists.");

        var passwordHash = passwordHasher.Hash(request.Password);
        var (publicKey, privateKey) = encryptionService.GenerateKeyPair(request.Password);

        var user = User.Create(request.Email, passwordHash, publicKey, privateKey);
        var vault = Vault.Create("My First Vault", user.Id, string.Empty);
        var auditLog = AuditLog.Record(user.Id, AuditAction.UserRegistered, nameof(User), user.Id);

        db.Users.Add(user);
        db.Vaults.Add(vault);
        db.AuditLogs.Add(auditLog);

        await db.SaveChangesAsync(cancellationToken);

        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();

        return Result.Success(new AuthResponse(accessToken, refreshToken, user.Id));
    }
}
