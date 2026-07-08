using FluentValidation;

namespace SecretManager.Application.Features.Vaults.Commands.CreateVault;

public class CreateVaultCommandValidator : AbstractValidator<CreateVaultCommand>
{
    public CreateVaultCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.MasterKey).NotEmpty();
        RuleFor(x => x.OwnerId).NotEmpty();
    }    
}