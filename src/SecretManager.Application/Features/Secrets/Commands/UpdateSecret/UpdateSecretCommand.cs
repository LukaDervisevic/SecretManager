using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Secrets.Commands.UpdateSecret;

public record UpdateSecretCommand(
    Guid SecretId, string Name, string CiphertextBlob
    ) : IRequest<Result>;