using System.ComponentModel.DataAnnotations;
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

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public string? FilePath { get; set; }
    public string UserId { get; set; }
    public User.User User { get; set; } = null!;

    [NotMapped]
    public string? Slug { get; }
}
