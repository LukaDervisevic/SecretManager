using FluentValidation;

namespace SecretManager.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationCommand>
{
    public CreateOrganizationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.OwnerId).NotEmpty();
    }
}