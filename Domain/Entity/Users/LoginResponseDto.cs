namespace Domain.Entity.Auth;

public record LoginResponseDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
