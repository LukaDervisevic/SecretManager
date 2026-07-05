using FluentValidation;

namespace SecretManager.Application.Features.Vaults.Queries.ListOrganizationsVaults;

public class ListOrganizationsVaultsValidator : AbstractValidator<ListOrganizationsVaultsQuery>
{
    public ListOrganizationsVaultsValidator()
    {
        RuleFor(x => x.OrganizationId).NotEmpty();
    }
}