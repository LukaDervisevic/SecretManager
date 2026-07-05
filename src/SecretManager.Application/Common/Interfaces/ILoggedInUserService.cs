namespace SecretManager.Application.Common.Interfaces;

public interface ILoggedInUserService
{
    Guid UserId { get; }
    string? IpAddress { get; }
}