using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface IProizvodRepository
{
    Proizvod Update(Proizvod proizvod);
}