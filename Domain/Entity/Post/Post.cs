using System.ComponentModel.DataAnnotations.Schema;
using Domain.Abstraction;

namespace Domain.Entity.Post;

public class Post : PostModel
{
    public Post() => Slug = Title?.Trim().Replace(" ", "-");

    public Guid Id { get; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public string? FilePath { get; set; }

    [NotMapped]
    public string? Slug { get; }
}
