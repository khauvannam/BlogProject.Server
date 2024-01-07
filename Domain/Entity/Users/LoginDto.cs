using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Users;

public class LoginDto
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}
