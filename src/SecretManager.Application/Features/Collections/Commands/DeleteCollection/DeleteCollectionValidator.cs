using FluentValidation;

namespace SecretManager.Application.Features.Collections.Commands.DeleteCollection;

public class DeleteCollectionValidator : AbstractValidator<DeleteCollectionCommand>
{
    public DeleteCollectionValidator()
    {
        RuleFor(x => x.CollectionId).NotEmpty();
    }
}