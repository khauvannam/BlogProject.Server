namespace Domain.Entity.Auth;

public record TokenDto(string AccessToken, string? RefreshToken);
