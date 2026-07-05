using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Collections.Commands.CreateCollection;

public record CreateCollectionCommand(
    string Name,
    Guid VaultId,
    Guid OwnerId,
    Guid? OrganizationId) :IRequest<Result<Guid>>;
    
