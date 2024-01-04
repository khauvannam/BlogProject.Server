namespace Domain.Entity.User;

public class LoginDto
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}
