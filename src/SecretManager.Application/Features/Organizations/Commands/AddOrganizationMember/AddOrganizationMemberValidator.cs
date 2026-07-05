using FluentValidation;

namespace SecretManager.Application.Features.Organizations.Commands.AddOrganizationMember;

public class AddOrganizationMemberValidator : AbstractValidator<AddOrganizationMemberCommand>
{
    public AddOrganizationMemberValidator()
    {
        RuleFor(x => x.OrganizationId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
    
}