using Microsoft.EntityFrameworkCore;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class UserRepository(AppDbContext db): IUserRepository
{
    public void Add(User user) => 
        db.Users.Add(user);

    public Task<User?> FindByEmail(string email, CancellationToken cancellationToken) => 
        db.Users.FirstOrDefaultAsync(u => u.Email == email,cancellationToken);

    public Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken) =>
        db.Users.FirstOrDefaultAsync(u => u.Id == userId,cancellationToken);
}