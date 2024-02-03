using System.Security.Claims;
using Application.Abstraction;
using Blog_Api.Abstractions;
using Domain.Entity.Auth;

namespace Infrastructure.Repository;

public class TokenRepository : ITokenRepository
{
    private readonly UserDbContext _context;
    private readonly IJwtHandler _jwtHandler;

    public TokenRepository(UserDbContext context, IJwtHandler jwtHandler)
    {
        _context = context;
        _jwtHandler = jwtHandler;
    }

    private void CheckExistingUser(string? userId)
    {
        var existingUser = _context.Users.Any(e => e.Id == userId);
        if (!existingUser)
            throw new Exception("Invalid request: User doesn't exist");
    }

    private Token CheckValidToken(string? userId, string? refreshToken)
    {
        var userToken = _context.Tokens.FirstOrDefault(e => e.UserId == userId);
        if (
            userToken is null
            || userToken.RefreshToken != refreshToken
            || userToken.ExpiredIn <= DateTime.Now
        )
            throw new Exception("Invalid token request: Tokens invaluable");
        return userToken;
    }

    public async Task<TokenDto> Refresh(TokenDto tokenDto)
    {
        var accessToken = tokenDto.AccessToken;
        var refreshToken = tokenDto.RefreshToken;
        var principal = _jwtHandler.GetClaimsPrincipalFromExpiredToken(accessToken);
        var userId = principal.FindFirstValue(nameof(ClaimTypes.NameIdentifier));

        CheckExistingUser(userId);
        var userToken = CheckValidToken(userId, refreshToken);

        var newAccessToken = _jwtHandler.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _jwtHandler.GenerateRefreshToken();
        userToken.RefreshToken = newRefreshToken;
        await _context.SaveChangesAsync();
        return new TokenDto(newAccessToken, newRefreshToken);
    }

    public async Task Revoke(string id)
    {
        var userToken = _context.Tokens.FirstOrDefault(e => e.UserId == id);
        userToken.RefreshToken = null;
        await _context.SaveChangesAsync();
    }
}
