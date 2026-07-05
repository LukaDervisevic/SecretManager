using FluentValidation;

namespace SecretManager.Application.Features.Secrets.Commands.DeleteSecret;

public class DeleteSecretValidator : AbstractValidator<DeleteSecretCommand>
{
    public DeleteSecretValidator()
    {
        RuleFor(x => x.SecretId).NotEmpty();
    }
}