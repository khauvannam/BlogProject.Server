using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PostRepository : IPostRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IHttpContextAccessor _contextAccessor;

    public PostRepository(
        UserDbContext context,
        IMapper mapper,
        IFileService fileService,
        IHttpContextAccessor contextAccessor
    )
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
        _contextAccessor = contextAccessor;
    }

    private async Task<string> GenerateFilePath(IFormFile file)
    {
        var blobFile = await _fileService.UploadAsync(file);
        return blobFile.Blob.Uri ?? string.Empty;
    }

    public async Task<Post> CreatePost(CreatePostDto createPostDto)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(
            nameof(ClaimTypes.NameIdentifier)
        );
        var post = _mapper.Map<CreatePostDto, Post>(createPostDto);
        if (createPostDto is { FileUpload: not null })
        {
            var filePath = await GenerateFilePath(createPostDto.FileUpload);
            post.MainImage = filePath;
        }

        post.UserId = userId;
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePost(string id)
    {
        var post =
            _context.Posts.FirstOrDefault(x => x.Id == id)
            ?? throw new Exception("Posts not available");
        var fileName = Path.GetFileNameWithoutExtension(post.MainImage);
        await _fileService.DeleteAsync(fileName);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Post>> GetAllPosts()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return await _context.Posts
            .Where(post => post.UserId == userId || post.Public)
            .Select(
                post =>
                    new Post()
                    {
                        Title = post.Title,
                        Content = post.Content,
                        MainImage = post.MainImage,
                        LastModified = post.LastModified
                    }
            )
            .ToListAsync();
    }

    public async Task<Post> GetsPostById(string id)
    {
        return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Can not find any post");
    }

    public async Task<Post> UpdatePost(EditPostDto editPostDto)
    {
        var post =
            _context.Posts.FirstOrDefault(x => x.Id == editPostDto.Id)
            ?? throw new Exception("Post not available");
        var fileName = Path.GetFileNameWithoutExtension(post.MainImage);
        _mapper.Map(editPostDto, post);
        if (fileName != editPostDto.FileUpload?.FileName && editPostDto.FileUpload is not null)
        {
            await _fileService.DeleteAsync(fileName);
            var newFilePath = await GenerateFilePath(editPostDto.FileUpload);
            post.MainImage = newFilePath;
        }

        await _context.SaveChangesAsync();
        return post;
    }
}
