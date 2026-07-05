using FluentValidation;

namespace SecretManager.Application.Features.Secrets.Commands.UpdateSecret;

public class UpdateSecretValidator : AbstractValidator<UpdateSecretCommand>
{
    public UpdateSecretValidator()
    {
        RuleFor(x => x.SecretId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.CiphertextBlob).NotEmpty();
    }
}