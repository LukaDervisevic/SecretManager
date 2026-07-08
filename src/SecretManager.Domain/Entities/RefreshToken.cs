namespace SecretManager.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public  string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRevoked { get; set; }
    
    private RefreshToken()
    {
    }
    public static RefreshToken Create(Guid userId, string token, int expiryDays = 30)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(expiryDays),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };
    }
    
    public bool IsActive => !IsRevoked && ExpiresAt > DateTime.UtcNow;
    public void Revoke() => IsRevoked = true;

}