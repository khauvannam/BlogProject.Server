using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PostRepository : IPostRepository
{
    private readonly SocialDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private async Task<string> GenerateFilePath(IFormFile file)
    {
        var blobFile = await _fileService.UploadAsync(file);
        return blobFile.Blob.Uri ?? string.Empty;
    }

    public PostRepository(
        SocialDbContext context,
        IMapper mapper,
        IFileService fileService,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Post> CreatePost(CreatePostDto createPostDto)
    {
        var userId =
            _httpContextAccessor.HttpContext?.User.FindFirstValue(claimType: "UserIdentity")
            ?? string.Empty;
        var post = _mapper.Map<CreatePostDto, Post>(createPostDto);
        if (createPostDto is { FileUpload: not null })
        {
            var filePath = await GenerateFilePath(createPostDto.FileUpload);
            post.FilePath = filePath;
        }

        post.UserId = userId;
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePost(string id)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == id);
        if (post is null)
            throw new Exception("Post not available");
        var fileName = Path.GetFileNameWithoutExtension(post.FilePath);
        if (fileName != null)
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
        return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Can not find any post");
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
