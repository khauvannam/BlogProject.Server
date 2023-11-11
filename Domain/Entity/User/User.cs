using Microsoft.AspNetCore.Identity;

namespace Domain.Entity.User;

public class User : IdentityUser<Guid>
{
    public List<Post.Post> Posts { get; } = null!;
}
