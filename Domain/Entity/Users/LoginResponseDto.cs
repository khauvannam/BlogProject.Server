namespace Domain.Entity.Auth;

public record LoginResponseDto
{
    public bool IsLoginSuccessful { get; set; } = true;
    public string? AccessToken { get; set; }
    public string? ErrorMessage { get; set; }
}
