using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SocialDbContext : DbContext
{
    public SocialDbContext(DbContextOptions opt) : base(opt)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
}