namespace SecretManager.Application.Auth.Commands.Register;

public record AuthResponse(string AccessToken, string RefreshToken, Guid UserId);
