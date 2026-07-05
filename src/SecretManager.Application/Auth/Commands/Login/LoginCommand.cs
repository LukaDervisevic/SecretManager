using MediatR;
using SecretManager.Application.Auth.Commands.Register;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<AuthResponse>>;