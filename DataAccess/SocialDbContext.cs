using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SocialDbContext : DbContext
{
    public SocialDbContext(DbContextOptions<SocialDbContext> opt)
        : base(opt) { }

    public DbSet<Post>? Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
    }
}
