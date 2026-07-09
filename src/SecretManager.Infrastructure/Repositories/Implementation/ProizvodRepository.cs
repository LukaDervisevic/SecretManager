using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class ProizvodRepository(AppDbContext db): IProizvodRepository
{
    public Proizvod Update(Proizvod proizvod)
    {
        return db.Update(proizvod).Entity;
    }
}