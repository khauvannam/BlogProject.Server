using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class AuthService
{
    private string UserId { get; set; }

    public string GetUserId()
    {
        return UserId;
    }

    public void SetUserId(string? userId)
    {
        UserId = userId;
    }
}
