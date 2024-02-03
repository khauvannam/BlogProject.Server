using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstraction;
using Blog_Api.Abstractions;
using Domain.Enum;
using Infrastructure.Abstraction;
using Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;

namespace Blog_Api.Identity;

public class JwtHandler : IJwtHandler
{
    private readonly string _secret = SecretService.GetSecret(nameof(Secret.jwtsecret));

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_secret);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var signinCredentials = GetSigningCredentials();
        var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:7149",
            audience: "https://localhost:7149",
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }

    public string? GenerateRefreshToken()
    {
        var token = Guid.NewGuid().ToString();
        return token;
    }

    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(nameof(Secret.jwtsecret))
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out var securityToken
        );
        if (securityToken is null)
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
