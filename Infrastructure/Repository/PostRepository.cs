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

    public async Task<Result<Post>> CreatePost(CreatePostDto createPostDto)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(
            nameof(ClaimTypes.NameIdentifier)
        );
        if (userId is null)
        {
            return UserErrors.NotFound;
        }
        var post = _mapper.Map<CreatePostDto, Post>(createPostDto);

        foreach (
            var postTag in createPostDto.TagId.Select(
                tag => new PostTag { PostId = post.Id, TagId = tag }
            )
        )
        {
            _context.PostsTags.Add(postTag);
        }

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

    public async Task<Result<string>> DeletePost(string id)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == id);
        if (post is null)
            return PostErrors.Nullable;
        var fileName = Path.GetFileNameWithoutExtension(post.MainImage);
        await _fileService.DeleteAsync(fileName);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return post.Id;
    }

    public async Task<Result<ICollection<Post>>> GetAllPosts()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return UserErrors.NotFound;
        }
        var posts = await _context.Posts
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
            .Include(e => e.User)
            .ToListAsync();
        return posts;
    }

    public async Task<Result<Post>> GetsPostById(string id)
    {
        var post = await _context.Posts
            .Include(e => e.Comments)!
            .ThenInclude(e => e.User)
            .Include(e => e.User)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (post is null)
            return PostErrors.Nullable;
        return post;
    }

    public async Task<Result<Post>> EditPost(EditPostDto editPostDto)
    {
        //TODO change the tags when edit post
        var post = _context.Posts.FirstOrDefault(x => x.Id == editPostDto.Id);
        if (post is null)
        {
            return PostErrors.Nullable;
        }
        var fileName = Path.GetFileNameWithoutExtension(post.MainImage);
        _mapper.Map(editPostDto, post);

        if (editPostDto is { FileUpload: not null })
        {
            await _fileService.DeleteAsync(fileName);
            var newFilePath = await GenerateFilePath(editPostDto.FileUpload);
            post.MainImage = newFilePath;
        }

        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Result<ICollection<Post>>> GetAllPostByTags(List<string>? tags)
    {
        if (tags is null)
        {
            return TagErrors.Nullable;
        }
        var posts = await _context.Posts
            .Where(p => p.PostTags.Any(pt => tags.Contains(pt.TagId)))
            .ToListAsync();

        return posts;
    }
}
