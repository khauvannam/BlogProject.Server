using Domain.Entity.Post;
using Domain.Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public UserDbContext(DbContextOptions<UserDbContext> opt)
        : base(opt) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>().HasKey(e => e.Id);
        modelBuilder
            .Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}
