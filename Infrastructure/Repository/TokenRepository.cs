using System.Security.Claims;
using Application.Abstraction;
using Application.Error;
using Blog_Api.Abstractions;
using Domain.Entity.Auth;
using Domain.Entity.ErrorsHandler;

namespace Infrastructure.Repository;

public class TokenRepository(UserDbContext context, IJwtHandler jwtHandler) : ITokenRepository
{
    public async Task<Result<TokenDto>> Refresh(ClaimsPrincipal principal, Token userToken)
    {
        var newAccessToken = jwtHandler.GenerateAccessToken(principal.Claims);
        var newRefreshToken = jwtHandler.GenerateRefreshToken();
        userToken.RefreshToken = newRefreshToken;
        userToken.ExpiredIn = DateTime.Now.AddMonths(1);
        await context.SaveChangesAsync();
        return new TokenDto(newAccessToken, newRefreshToken);
    }

    public async Task<Result<string>> Revoke(string id)
    {
        var userToken = context.Tokens.FirstOrDefault(e => e.UserId == id);
        if (userToken is null)
        {
            return TokenErrors.Invalid;
        }
        userToken.RefreshToken = null;
        await context.SaveChangesAsync();
        return string.Empty;
    }
}
