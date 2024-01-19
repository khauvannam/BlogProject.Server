using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Comments;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly IMapper _mapper;
    private readonly UserDbContext _dbContext;
    private readonly IHttpContextAccessor _accessor;

    public CommentRepository(IMapper mapper, UserDbContext dbContext, IHttpContextAccessor accessor)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _accessor = accessor;
    }

    private string CheckUser()
    {
        var userId = _accessor.HttpContext?.User.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
        if (userId is null)
            throw new Exception("Your user is null, try again");
        return userId;
    }

    public async Task CreateComment(CommentDto commentDto)
    {
        var userId = _accessor.HttpContext?.User.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
        var comment = _mapper.Map<CommentDto, Comment>(commentDto);
        comment.UserId = userId;
        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteComment(string id)
    {
        var userId = CheckUser();
        var comment = _dbContext.Comments.FirstOrDefault(e => e.CommentId == id);
        if (comment.UserId != userId)
            throw new Exception("Invalid request: You can't remove the other's comment");
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditComment(EditCommentDto editCommentDto)
    {
        var userId = CheckUser();
        var comment = _dbContext.Comments.FirstOrDefault(e => e.CommentId == editCommentDto.Id);
        if (comment.UserId != userId)
            throw new Exception("Invalid request: You can't edit the other's comment");
        _mapper.Map<EditCommentDto, Comment>(editCommentDto);
        await _dbContext.SaveChangesAsync();
    }
}
