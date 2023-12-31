﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;

namespace Blog_Api.Identity;

public class JwtHandler
{
    private readonly string _secret = SecretService.GetSecret(nameof(Domain.Enum.Secret.jwtsecret));

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

    public string GenerateRefreshToken()
    {
        var token = Guid.NewGuid().ToString();
        return token;
    }
}
