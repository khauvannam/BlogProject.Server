using System.Security.Claims;
using Application.Abstraction;
using Domain.Entity.Post;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class PostRepository : IPostRepository
{
    private readonly SocialDbContext _context;
    private readonly IFileService _fileService;

    public PostRepository(SocialDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Post> CreatePost(CreatePostDto createPostDto, ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(claimType: "UserIdentity") ?? string.Empty;
        var blobFile = await _fileService.UploadAsync(createPostDto.FileUpload);
        var fileUploadName = blobFile.Blob.Uri;
        var post = new Post
        {
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            FilePath = fileUploadName,
            UserId = userId
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public Task<Post> CreatePost(CreatePostDto createPost)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePost(string id)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == id);
        var fileName = Path.GetFileNameWithoutExtension(post.FilePath);
        if (post is null)
            return;
        await _fileService.DeleteAsync(fileName);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Post>> GetAllPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post> GetsPostById(string id)
    {
        return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Post> UpdatePost(EditPostDto editPostDto)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == editPostDto.Id);
        var fileName = Path.GetFileNameWithoutExtension(post.FilePath);
        post.LastModified = DateTime.Now;
        post.Title = editPostDto.Title;
        post.Content = editPostDto.Content;
        if (fileName != editPostDto.FileUpload?.FileName)
        {
            await _fileService.DeleteAsync(fileName);
            var blobFile = await _fileService.UploadAsync(editPostDto.FileUpload);
            var newFilePath = blobFile.Blob.Uri;
            post.FilePath = newFilePath;
        }

        await _context.SaveChangesAsync();
        return post;
    }
}
