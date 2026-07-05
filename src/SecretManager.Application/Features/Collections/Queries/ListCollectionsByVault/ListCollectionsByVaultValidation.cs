using FluentValidation;

namespace SecretManager.Application.Features.Collections.Queries.ListCollectionsByVault;

public class ListCollectionsByVaultValidation : AbstractValidator<ListCollectionsByVaultQuery>
{
    public ListCollectionsByVaultValidation()
    {
        RuleFor(x => x.VaultId).NotEmpty();
    }
}