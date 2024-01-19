using System.Security.Claims;

namespace Application.Abstraction;

public interface IJwtHandler
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string? GenerateRefreshToken();
    ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
}
