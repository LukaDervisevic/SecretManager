using MediatR;
using SecretManager.Application.Auth.Commands.Register;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IUnitOfWork uow, ITokenService tokenService): IRequestHandler<RefreshTokenCommand,Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await uow.RefreshTokenRepository.GetTokenAsync(request.RefreshToken, cancellationToken);
        if (storedToken is null || !storedToken.IsActive)
            return Result.Failure<AuthResponse>("nvalid or expired refresh token.");

        var user = await uow.UserRepository.FindByIdAsync(storedToken.UserId, cancellationToken);
        if (user is null)
            return Result.Failure<AuthResponse>("User not found");
        
        storedToken.Revoke();

        var newAccessToken = tokenService.GenerateAccessToken(user);
        var newRefreshToken = tokenService.GenerateRefreshToken();
        var newRefreshTokenEntity = Domain.Entities.RefreshToken.Create(user.Id, newRefreshToken);
        
        uow.RefreshTokenRepository.Add(newRefreshTokenEntity);
        uow.RefreshTokenRepository.Update(storedToken);
        await uow.SaveChangesAsync(cancellationToken);

        return Result.Success(new AuthResponse(newAccessToken, newRefreshToken, user.Id, user.MasterPasswordSalt));
        
    }
}