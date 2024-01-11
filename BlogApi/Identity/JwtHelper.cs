using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Blog_Api.Identity;

public static class JwtHelper
{
    private static TokenValidationParameters GetTokenValidationParameters(string jwtSecret)
    {
        return new TokenValidationParameters
        {
            ValidAudience = "https://localhost:7149",
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidIssuer = "https://localhost:7149",
            ValidateIssuer = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    }

    public static void ConfigureBearerOption(this JwtBearerOptions options, string jwtSecret)
    {
        options.SaveToken = true;
        options.TokenValidationParameters = GetTokenValidationParameters(jwtSecret);
    }
}
