using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.User;

public class User : IdentityUser<string>
{
    public User() => Id = Guid.NewGuid().ToString();

    public List<Post.Post> Posts { get; } = null!;
}
