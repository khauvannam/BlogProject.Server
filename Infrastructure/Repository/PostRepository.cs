using System.Security.Claims;
using Application.Abstraction;
using Application.Error;
using AutoMapper;
using Domain.Entity.ErrorsHandler;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using Domain.Entity.PostsTags;
using Infrastructure.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PostRepository(
    UserDbContext context,
    IMapper mapper,
    IFileService fileService,
    IHttpContextAccessor contextAccessor)
    : IPostRepository
{
    private async Task<string> GenerateFilePath(IFormFile file)
    {
        var blobFile = await fileService.UploadAsync(file);
        return blobFile.Blob.Uri ?? string.Empty;
    }

    public async Task<Result<Post>> CreatePost(CreatePostDto createPostDto)
    {
        var userId = contextAccessor.HttpContext?.User.FindFirstValue(
            nameof(ClaimTypes.NameIdentifier)
        );
        if (userId is null)
        {
            return UserErrors.NotFound;
        }
        var post = mapper.Map<CreatePostDto, Post>(createPostDto);

        foreach (
            var postTag in createPostDto.TagId.Select(
                tag => new PostTag { PostId = post.Id, TagId = tag }
            )
        )
        {
            context.PostsTags.Add(postTag);
        }

        if (createPostDto is { FileUpload: not null })
        {
            var filePath = await GenerateFilePath(createPostDto.FileUpload);
            post.MainImage = filePath;
        }

        post.UserId = userId;
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        return post;
    }

    public async Task<Result<string>> DeletePost(string id)
    {
        var post = context.Posts.FirstOrDefault(p => p.Id == id);
        if (post is null)
            return PostErrors.Nullable;
        var fileName = Path.GetFileNameWithoutExtension(post.MainImage);
        await fileService.DeleteAsync(fileName);
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
        return post.Id;
    }

    public async Task<Result<ICollection<Post>>> GetAllPosts()
    {
        var userId = contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return UserErrors.NotFound;
        }
        var posts = await context.Posts
            .Where(post => post.UserId == userId || post.Public)
            .Include(p => p.User)
            .Select(
                post =>
                    new Post()
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Content = post.Content,
                        MainImage = post.MainImage,
                        LastModified = post.LastModified,
                        User = post.User,
                    }
            )
            .Include(p => p.User)
            .ToListAsync();
        return posts;
    }

    public async Task<Result<Post>> GetsPostById(string id)
    {
        var post = await context.Posts
            .Include(p => p.Comments)!
            .ThenInclude(p => p.User)
            .Include(p => p.User)
            .Include(p => p.PostTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);
        return post is null ? PostErrors.Nullable : post;
    }

    public async Task<Result<Post>> EditPost(EditPostDto editPostDto)
    {
        //TODO change the tags when edit post
        var post = context.Posts.FirstOrDefault(p => p.Id == editPostDto.Id);
        if (post is null)
        {
            return PostErrors.Nullable;
        }
        var fileName = Path.GetFileNameWithoutExtension(post.MainImage);
        mapper.Map(editPostDto, post);

        if (editPostDto is { FileUpload: not null })
        {
            await fileService.DeleteAsync(fileName);
            var newFilePath = await GenerateFilePath(editPostDto.FileUpload);
            post.MainImage = newFilePath;
        }

        await context.SaveChangesAsync();
        return post;
    }

    public async Task<Result<ICollection<Post>>> GetAllPostByTags(List<string>? tags)
    {
        if (tags is null)
        {
            return TagErrors.Nullable;
        }
        var posts = await context.Posts
            .Where(p => p.PostTags.Any(pt => tags.Contains(pt.TagId)))
            .ToListAsync();

        return posts;
    }
}
