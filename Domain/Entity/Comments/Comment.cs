namespace Domain.Entity.Comments;

public class Comment
{
    public string CommentId { get; set; } = Guid.NewGuid().ToString();
    public string? Content { get; set; }
    public string? UserId { get; set; }
    public Users.User? User { get; set; }
    public string? PostId { get; set; }
    public Posts.Post? Post { get; set; }
}
