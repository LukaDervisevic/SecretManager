using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using SecretManager.Application.Common.Interfaces;

namespace SecretManager.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ILoggedInUserService
{
    public Guid UserId
    {
        get
        {
            var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)
                        ?? httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub);

            if (claim is null || !Guid.TryParse(claim.Value, out var userId))
                throw new UnauthorizedAccessException("User is not authenticated");

            return userId;
        }
    }

    public string? IpAddress =>
        httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
}