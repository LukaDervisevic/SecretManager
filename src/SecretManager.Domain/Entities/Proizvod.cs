namespace SecretManager.Domain.Entities;

public class Proizvod
{
    public Guid Id { get; set; }
    public string Naziv { get; set; }
    public double Cena { get; set; }

    public static Proizvod Create(Guid id, string naziv, double cena)
    {
        return new Proizvod
        {
            Id = id,
            Naziv = naziv,
            Cena = cena
        };
    }
    
}