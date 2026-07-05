using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Models;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Features.Collections.Queries.ListCollectionsByVault;

public record ListCollectionsByVaultQuery(Guid VaultId): IRequest<Result<List<CollectionDto>>>;
