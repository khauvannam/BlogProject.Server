using Domain.Entity.Comments;
using Domain.Entity.Posts;
using Domain.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public UserDbContext(DbContextOptions<UserDbContext> opt)
        : base(opt) { }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region post table

        modelBuilder.Entity<Post>().HasKey(e => e.Id);

        modelBuilder
            .Entity<Post>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Post>()
            .HasOne<User>(e => e.User)
            .WithMany(e => e.Posts)
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region user table

        modelBuilder
            .Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region comment table

        modelBuilder.Entity<Comment>().HasKey(e => e.CommentId);
        modelBuilder
            .Entity<Comment>()
            .HasOne(e => e.User)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Comment>()
            .HasOne(e => e.Post)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
    }
}
