using Domain.Entity.Comments;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.Users;

public sealed class User : IdentityUser<string>
{
    public User() => Id = Guid.NewGuid().ToString();

    public IQueryable<Posts.Post>? Posts { get; init; }
    public IQueryable<Comment>? Comments { get; init; }
}
