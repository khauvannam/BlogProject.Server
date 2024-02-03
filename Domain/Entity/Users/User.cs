using Domain.Entity.Auth;
using Domain.Entity.Comments;
using Domain.Entity.Favourite;
using Domain.Entity.History;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.Users;

public sealed class User : IdentityUser<string>
{
    public User() => Id = Guid.NewGuid().ToString();

    public Token Token { get; set; } = null!;
    public IQueryable<Posts.Post>? Posts { get; init; }
    public IQueryable<Comment>? Comments { get; init; }
    public IQueryable<FavouritePosts>? FavouritePostsList { get; set; }
    public IQueryable<HistoryPosts>? HistoryPostsList { get; set; }
}
