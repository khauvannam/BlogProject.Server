using System.Security.Claims;

namespace Blog_Api.Abstractions;

public interface IJwtHandler
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string? GenerateRefreshToken();
    ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
}
