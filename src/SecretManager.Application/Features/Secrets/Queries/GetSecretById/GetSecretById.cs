using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Secrets.Queries.GetSecretById;

public record GetSecretById(Guid SecretId): IRequest<Result<SecretDto>>;