using FluentValidation;

namespace SecretManager.Application.Features.Collections.Commands.CreateCollection;

public class CreateCollectionValidator : AbstractValidator<CreateCollectionCommand>
{
    public CreateCollectionValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.VaultId).NotEmpty();
    }
}