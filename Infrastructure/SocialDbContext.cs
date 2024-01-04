using Domain.Entity.Post;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SocialDbContext : DbContext
{
    public SocialDbContext(DbContextOptions<SocialDbContext> opt)
        : base(opt) { }

    public DbSet<Post> Posts { get; set; } = null!;
}
