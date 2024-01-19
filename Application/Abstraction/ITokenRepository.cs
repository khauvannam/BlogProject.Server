using Domain.Entity.Auth;

namespace Application.Abstraction;

public interface ITokenRepository
{
    Task<TokenDto> Refresh(TokenDto token);
    Task Revoke(string id);
}
