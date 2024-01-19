using System.Security.Claims;
using Domain.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Blog_Api.Identity;

public static class IdentityConfig
{
    public static void ConfigureAuthorization(this AuthorizationOptions options)
    {
        options.AddPolicy(
            "Users",
            policy =>
                policy.RequireClaim(
                    nameof(ClaimTypes.Role),
                    nameof(Role.BasicUser),
                    nameof(Role.PremiumUser),
                    nameof(Role.Admin)
                )
        );
        options.AddPolicy(
            "Admin",
            policy => policy.RequireClaim(nameof(ClaimTypes.Role), nameof(Role.Admin))
        );
    }

    public static void ConfigureAuthOptions(this AuthenticationOptions options)
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    public static void AddIdentityOptions(this IdentityOptions options)
    {
        options.Password = new PasswordOptions
        {
            RequireLowercase = false,
            RequireUppercase = false,
            RequireDigit = false,
            RequireNonAlphanumeric = false
        };
        options.User = new UserOptions()
        {
            AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
            RequireUniqueEmail = true,
        };
        options.Lockout = new LockoutOptions() { AllowedForNewUsers = true };
    }
}
