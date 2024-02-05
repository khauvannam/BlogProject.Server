using System.Security.Claims;
using Application.Error;
using Domain.Entity.Auth;

namespace Application.Abstraction;

public interface IUserServiceRepository
{
    public ClaimsPrincipal GetClaims(string accessToken);
    public Result<string> CheckExistingUser(ClaimsPrincipal claimsPrincipal);
    public Result<Token> CheckValidToken(string? userId, string? refreshToken);
}
