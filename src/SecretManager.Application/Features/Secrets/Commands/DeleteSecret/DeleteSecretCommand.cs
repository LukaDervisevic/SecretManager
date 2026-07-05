using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Secrets.Commands.DeleteSecret;

public record DeleteSecretCommand(Guid SecretId) : IRequest<Result>;