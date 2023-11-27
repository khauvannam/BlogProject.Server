using System.ComponentModel.DataAnnotations.Schema;
using Domain.Abstraction;

namespace Domain.Entity.Post;

public class Post : PostModel<string>
{
    public Post()
    {
        Slug = Title?.Trim().Replace(" ", "-");
        Id = Guid.NewGuid().ToString();
    }

    public string UserId { get; set; } = null!;
    public User.User User { get; set; } = null!;
    public string? FilePath { get; set; }

    [NotMapped]
    public string? Slug { get; }
}
