using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SocialDbContext : DbContext
{
    public SocialDbContext(DbContextOptions<SocialDbContext> opt)
        : base(opt) { }

    public DbSet<Post> Posts { get; set; } = null!;
}
