namespace Domain.Models;

public class User
{
    public Guid Id { get; set; } = new Guid();
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public List<Post> Posts { get; set; }

}