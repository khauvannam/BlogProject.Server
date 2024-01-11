using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Comments;

namespace Domain.Entity.Posts;

public class Post
{
    public Post() => Slug = Title?.Trim().Replace(" ", "-");

    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool Public { get; set; }
    public string UserId { get; set; } = null!;
    public Users.User User { get; set; } = null!;
    public List<Comment>? Comments { get; set; }

    public string MainImage { get; set; } =
        "https://preview.redd.it/dk7lkcyt0nk31.jpg?auto=webp&s=4c55a671807f629e6af23ab5b56618b8d6a37b0f";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;

    [NotMapped]
    public string? Slug { get; set; }
}
