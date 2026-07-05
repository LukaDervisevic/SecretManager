using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}