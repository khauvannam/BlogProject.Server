using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Comments;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class CommentHub(UserDbContext dbContext, IMapper mapper) : Hub, ICommentRepository
{
    private string CheckUser()
    {
        var userId = Context.User?.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
        if (userId is null)
            throw new Exception("Your user is null, try again");
        return userId;
    }

    public async Task CreateComment(CommentDto commentDto)
    {
        var userId = CheckUser();
        var comment = mapper.Map<CommentDto, Comment>(commentDto);
        comment.UserId = userId;
        dbContext.Comments.Add(comment);
        await dbContext.SaveChangesAsync();
        await Clients.Group($"Post_{comment.PostId}").SendAsync("Receive Comment", comment);
    }

    public async Task DeleteComment(string id)
    {
        var userId = CheckUser();
        var comment = dbContext.Comments.FirstOrDefault(e => e.CommentId == id);
        if (comment?.UserId != userId)
            throw new Exception("Invalid request: You can't remove the other's comment");
        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
    }

    public async Task EditComment(EditCommentDto editCommentDto)
    {
        var userId = CheckUser();
        var comment = dbContext.Comments.FirstOrDefault(e => e.CommentId == editCommentDto.Id);
        if (comment?.UserId != userId)
            throw new Exception("Invalid request: You can't edit the other's comment");
        mapper.Map<EditCommentDto, Comment>(editCommentDto);
        await dbContext.SaveChangesAsync();
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
