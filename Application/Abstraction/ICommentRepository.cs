using Domain.Entity.Comments;

namespace Application.Abstraction;

public interface ICommentRepository
{
    Task CreateComment(CommentDto commentDto);
    Task DeleteComment(string id);
    Task EditComment(EditCommentDto editCommentDto);
}
