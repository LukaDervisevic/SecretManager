using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Auth.Commands.Register;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    IUnitOfWork uow,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    ILoggedInUserService currentUser)
    : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await uow.UserRepository.FindByEmail(request.Email.ToLowerInvariant().Trim(),cancellationToken);

        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result.Failure<AuthResponse>("Invalid email or password");

        var auditLog = AuditLog.Record(user.Id, AuditAction.UserLoggedIn, nameof(User), user.Id, currentUser.IpAddress);
        uow.AuditLogRepository.Add(auditLog);
        await uow.SaveChangesAsync(cancellationToken);

        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken();

        return Result.Success(new AuthResponse(accessToken,refreshToken,user.Id,user.MasterPasswordSalt));
    }
}