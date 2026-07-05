using FluentValidation;

namespace SecretManager.Application.Features.Secrets.Commands.CreateSecret;

public class CreateSecretCommandValidator : AbstractValidator<CreateSecretCommand>
{
    public CreateSecretCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Ciphertextblob).NotEmpty();
        RuleFor(x => x.VaultId).NotEmpty();
        RuleFor(x => x.Type);
    }
}