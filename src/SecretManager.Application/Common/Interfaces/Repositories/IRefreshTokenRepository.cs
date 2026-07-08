using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetTokenAsync(string token, CancellationToken cancellationToken = default);
    void Add(RefreshToken refreshToken);
    void Update(RefreshToken refreshToken);
}