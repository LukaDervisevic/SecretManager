using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class RefreshTokenRepository(AppDbContext db): IRefreshTokenRepository
{
    public Task<RefreshToken?> GetTokenAsync(string token, CancellationToken cancellationToken = default) =>
        db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token, cancellationToken);

    public void Add(RefreshToken refreshToken) =>
        db.RefreshTokens.Add(refreshToken);
    public void Update(RefreshToken refreshToken) =>
        db.RefreshTokens.Update(refreshToken);
}