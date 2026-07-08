using MediatR;
using SecretManager.Application.Auth.Commands.Register;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(
    string RefreshToken):IRequest<Result<AuthResponse>>;