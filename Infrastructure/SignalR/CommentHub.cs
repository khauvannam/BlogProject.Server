using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Comments;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class CommentHub : Hub, ICommentRepository
{
    private readonly UserDbContext _dbContext;
    private readonly IMapper _mapper;

    private string CheckUser()
    {
        var userId = Context.User?.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
        if (userId is null)
            throw new Exception("Your user is null, try again");
        return userId;
    }

    public CommentHub(UserDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task CreateComment(CommentDto commentDto)
    {
        var userId = CheckUser();
        var comment = _mapper.Map<CommentDto, Comment>(commentDto);
        comment.UserId = userId;
        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync();
        await Clients.Group($"Post_{comment.PostId}").SendAsync("Receive Comment", comment);
    }

    public async Task DeleteComment(string id)
    {
        var userId = CheckUser();
        var comment = _dbContext.Comments.FirstOrDefault(e => e.CommentId == id);
        if (comment?.UserId != userId)
            throw new Exception("Invalid request: You can't remove the other's comment");
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditComment(EditCommentDto editCommentDto)
    {
        var userId = CheckUser();
        var comment = _dbContext.Comments.FirstOrDefault(e => e.CommentId == editCommentDto.Id);
        if (comment?.UserId != userId)
            throw new Exception("Invalid request: You can't edit the other's comment");
        _mapper.Map<EditCommentDto, Comment>(editCommentDto);
        await _dbContext.SaveChangesAsync();
    }

    public async Task OnConnectedAsync(string postId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Post_{postId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
