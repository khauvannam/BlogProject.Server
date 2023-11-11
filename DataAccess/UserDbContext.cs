using Domain.Entity.Post;
using Domain.Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class UserDbContext : IdentityDbContext<IdentityUser>
{
    public UserDbContext(DbContextOptions<UserDbContext> opt)
        : base(opt) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder
            .Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Email)
            .IsRequired();
    }
}
