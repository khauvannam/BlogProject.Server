using System.Security.Claims;
using Application.Error;
using Domain.Entity.Auth;

namespace Application.Abstraction;

public interface ITokenRepository
{
    Task<Result<TokenDto>> Refresh(ClaimsPrincipal principal, Token userToken);
    Task<Result<string>> Revoke(string id);
}
