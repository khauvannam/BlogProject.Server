using Domain.Entity.Posts;
using Domain.Entity.Users;

namespace Domain.Abstraction;

public class PostsRelationship
{
    public string PostId { get; set; }
    public Post Post { get; set; }
    public string? UserId { get; set; }
    public User User { get; set; }
}
