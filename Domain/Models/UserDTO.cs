namespace Domain.Models;

public class UserDTO
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
}