using Application.Abstraction;
using Domain.Models;
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

    public async Task<Post> CreatePost(Post post)
    {
        post.CreatedAt = DateTime.Now;
        post.LastModified = DateTime.Now;
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePost(int id)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == id);
        string fileName = Path.GetFileNameWithoutExtension(post.FilePath);
        if (post is null) return;
        _fileService.DeleteAsync(fileName);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Post>> GetAllPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<Post> GetsPostById(int id)
    {
        return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Post> UpdatePost(string PostContent, int id)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == id);
        post.LastModified = DateTime.Now;
        post.Content = PostContent;
        await _context.SaveChangesAsync();
        return post;
    }
}