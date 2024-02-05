using System.Security.Claims;
using Application.Abstraction;
using Application.Error;
using Blog_Api.Abstractions;
using Domain.Entity.Auth;
using Domain.Entity.ErrorsHandler;

namespace Infrastructure.Repository;

public class UserServiceRepository(UserDbContext context, IJwtHandler jwtHandler) : IUserServiceRepository
{
    public ClaimsPrincipal GetClaims(string accessToken)
    {
        var principal = jwtHandler.GetClaimsPrincipalFromExpiredToken(accessToken);
        return principal;
    }

    public Result<string> CheckExistingUser(ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
        var userToken = context.Tokens.FirstOrDefault(e => e.UserId == userId);
        if (userToken is null)
        {
            return UserErrors.NotFound;
        }

        return userToken.UserId;
    }

    public Result<Token> CheckValidToken(string? userId, string? refreshToken)
    {
        var userToken = context.Tokens.FirstOrDefault(e => e.UserId == userId);
        if (
            userToken is null
            || userToken.RefreshToken != refreshToken
            || userToken.ExpiredIn <= DateTime.Now
        )
        {
            return TokenErrors.Invalid;
        }
        return userToken;
    }
}
