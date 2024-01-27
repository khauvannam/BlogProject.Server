using System.Security.Claims;
using Domain.Entity.History;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class HistoryHub : Hub
{
    private readonly UserDbContext _dbContext;
    private readonly string? _requestUser;

    public HistoryHub(UserDbContext dbContext)
    {
        _dbContext = dbContext;
        _requestUser = Context.User?.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
    }

    public async Task AddFavouritePost(string postId)
    {
        var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);
        if (post is null)
            throw new Exception("Can not find this post");
        _dbContext.HistoryPostsList.Add(
            new HistoryPosts { PostId = post.Id, UserId = _requestUser }
        );
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFavouritePost(string postId)
    {
        var historyPosts = _dbContext.HistoryPostsList.FirstOrDefault(
            fp => fp.PostId == postId && fp.UserId == _requestUser
        );
        if (historyPosts is not null)
        {
            _dbContext.HistoryPostsList.Remove(historyPosts);
            await _dbContext.SaveChangesAsync();
        }
    }
}
