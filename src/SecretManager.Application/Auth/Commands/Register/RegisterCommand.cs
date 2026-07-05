using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Auth.Commands.Register;

public record RegisterCommand(string Email, string Password) : IRequest<Result<AuthResponse>>;