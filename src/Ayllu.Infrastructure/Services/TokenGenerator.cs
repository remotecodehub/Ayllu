using Ayllu.Application.Common.Interfaces;
using Ayllu.Domain.Entities;
using Ayllu.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ayllu.Infrastructure.Services;

public class TokenGenerator(IConfiguration configuration, ILogger<TokenGenerator> logger, AppDbContext context) : ITokenGenerator
{
    public (string jwt, DateTime expirationDate, string refreshToken) GenerateToken(
        string username,
        string email,
        string organization,
        string phoneNumber,
        IList<string> roles,
        IList<string> organizationRoles)
    {
        try
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey missing");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiresInMinutes = int.Parse(jwtSettings["ExpiresInMinutes"] ?? "60");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(expiresInMinutes);

            // Claims básicos
            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Email, email),
            new("org", organization ?? string.Empty),
            new(JwtRegisteredClaimNames.PhoneNumber, phoneNumber ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];

            // Roles do sistema
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Roles dentro da organização
            foreach (var orgRole in organizationRoles)
            {
                claims.Add(new Claim("org_role", orgRole));
            }

            // Criar JWT
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expiration,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Criar Refresh Token
            var refreshToken = GenerateRefreshToken();

            return (jwt, expiration, refreshToken);

        }
        catch (Exception e)
        {
            logger.LogError(e, "Erro ao gerar token JWT: {Message}", e.Message);
            throw new InvalidOperationException("Erro ao gerar o token JWT", e);
        }
    }

    public void Invalidate(string jwt)
    {
        try
        {
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var expiration = token.ValidTo;

            var invalidatedToken = new InvalidatedToken
            {
                Jwt = jwt,
                ExpirationDate = expiration
            };

            context.InvalidatedTokens.Add(invalidatedToken);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Erro ao invalidar token: {Message}", e.Message);
            throw new InvalidOperationException("Erro ao invalidar token", e);
        }
    }

    public bool IsTokenInvalidated(string jwt)
    {
        return context.InvalidatedTokens.Any(t => t.Jwt == jwt);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
