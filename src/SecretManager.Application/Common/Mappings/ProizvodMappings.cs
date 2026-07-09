using SecretManager.Application.Common.Dtos;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Mappings;

public static class ProizvodMappings
{
    public static ProizvodDto ToDto(this Proizvod proizvod)
    {
        return new ProizvodDto(proizvod.Id, proizvod.Naziv, proizvod.Cena);
    }
}