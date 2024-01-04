using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Domain.Entity.User;

public class RegisterDto
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; init; } = null!;

    [Required]
    public string UserName { get; init; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; init; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; init; }

    public Role SetRole { get; set; }
}
