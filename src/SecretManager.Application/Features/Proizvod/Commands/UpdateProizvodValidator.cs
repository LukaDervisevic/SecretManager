using FluentValidation;

namespace SecretManager.Application.Features.Proizvod.Commands;

public class UpdateProizvodValidator: AbstractValidator<UpdateProizvodCommand>
{
    public UpdateProizvodValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Cena).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}