using System.Security.Claims;
using Domain.Entity.Favourite;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class FavouritePostHub : Hub
{
    private readonly UserDbContext _dbContext;
    private readonly string? _requestUser;

    public FavouritePostHub(UserDbContext dbContext)
    {
        _dbContext = dbContext;
        _requestUser = Context.User?.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
    }

    public async Task AddFavouritePost(string postId)
    {
        var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);
        if (post is null)
            throw new Exception("Can not find this post");
        _dbContext.FavouritePostsList.Add(
            new FavouritePosts { PostId = post.Id, UserId = _requestUser }
        );
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFavouritePost(string postId)
    {
        var favouritePost = _dbContext.FavouritePostsList.FirstOrDefault(
            fp => fp.PostId == postId && fp.UserId == _requestUser
        );
        if (favouritePost is not null)
        {
            _dbContext.FavouritePostsList.Remove(favouritePost);
            await _dbContext.SaveChangesAsync();
        }
    }
}
