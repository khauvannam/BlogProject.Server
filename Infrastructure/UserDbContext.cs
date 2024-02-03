using Domain.Entity.Auth;
using Domain.Entity.Comments;
using Domain.Entity.Favourite;
using Domain.Entity.History;
using Domain.Entity.Posts;
using Domain.Entity.PostsTags;
using Domain.Entity.Tags;
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
    public DbSet<Token> Tokens { get; set; } = null!;
    public DbSet<PostTag> PostsTags { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<FavouritePosts> FavouritePostsList { get; set; }
    public DbSet<HistoryPosts> HistoryPostsList { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region token table

        modelBuilder.Entity<Token>().HasKey(e => e.TokenId);
        modelBuilder
            .Entity<Token>()
            .HasOne(e => e.User)
            .WithOne(e => e.Token)
            .HasForeignKey<Token>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region post table

        modelBuilder.Entity<Post>().HasKey(e => e.Id);

        modelBuilder
            .Entity<Post>()
            .HasMany(e => e.FavouritePostsList)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Post>()
            .HasMany(e => e.HistoryPostsList)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Post>()
            .HasMany(e => e.PostTags)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Cascade);

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
            .HasOne(e => e.Token)
            .WithOne(e => e.User)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<User>()
            .HasMany(e => e.FavouritePostsList)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<User>()
            .HasMany(e => e.HistoryPostsList)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
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

        #region postTag table

        modelBuilder.Entity<PostTag>().HasKey(e => new { e.PostId, e.TagId });

        modelBuilder
            .Entity<PostTag>()
            .HasOne(e => e.Post)
            .WithMany(e => e.PostTags)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<PostTag>()
            .HasOne(e => e.Tag)
            .WithMany(e => e.PostTags)
            .HasForeignKey(e => e.TagId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region tag table

        modelBuilder.Entity<Tag>().HasKey(e => e.Id);
        modelBuilder
            .Entity<Tag>()
            .HasMany(e => e.PostTags)
            .WithOne(e => e.Tag)
            .HasForeignKey(e => e.TagId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region favourite post

        modelBuilder.Entity<FavouritePosts>().HasKey(e => new { e.PostId, e.UserId });

        modelBuilder
            .Entity<FavouritePosts>()
            .HasOne(e => e.Post)
            .WithMany(e => e.FavouritePostsList)
            .HasForeignKey(e => e.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<FavouritePosts>()
            .HasOne(e => e.User)
            .WithMany(e => e.FavouritePostsList)
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region history post

        modelBuilder.Entity<HistoryPosts>().HasKey(e => new { e.PostId, e.UserId });

        modelBuilder
            .Entity<HistoryPosts>()
            .HasOne(e => e.Post)
            .WithMany(e => e.HistoryPostsList)
            .HasForeignKey(e => e.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<HistoryPosts>()
            .HasOne(e => e.User)
            .WithMany(e => e.HistoryPostsList)
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}
