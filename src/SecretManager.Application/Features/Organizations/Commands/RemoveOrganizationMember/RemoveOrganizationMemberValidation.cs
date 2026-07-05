using FluentValidation;

namespace SecretManager.Application.Features.Organizations.Commands.RemoveOrganizationMember;

public class RemoveOrganizationMemberValidation : AbstractValidator<RemoveOrganizationMemberCommand>
{
    public RemoveOrganizationMemberValidation()
    {
        RuleFor(x => x.OrganizationId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}