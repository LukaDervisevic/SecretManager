using FluentValidation;

namespace SecretManager.Application.Auth.Commands.RefreshToken;

public class RefreshTokenValidator: AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}