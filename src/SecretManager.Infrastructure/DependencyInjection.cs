using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Infrastructure.Persistence;
using SecretManager.Infrastructure.Repositories.Implementation;
using SecretManager.Infrastructure.Security;

namespace SecretManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        services.AddScoped<IAppDbContext>(provider =>
            provider.GetRequiredService<AppDbContext>());

        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IEncryptionService, EncryptionService>();

        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<ISecretRepository, SecretRepository>();
        services.AddScoped<IVaultRepository,VaultRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IAuditLogRepository,AuditLogRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMemberRepository,MemberRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IProizvodRepository, ProizvodRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings");
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
                };
            });
        return services;
    }
}