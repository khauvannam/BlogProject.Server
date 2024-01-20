using Domain.Abstraction;

namespace Domain.Entity.Comments;

public class EditCommentDto : CommentDto
{
    public string? Id { get; set; }
}
