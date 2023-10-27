using Application.Abstraction;
using Domain.Models;
using Microsoft.AspNetCore.Http;
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
        var blobFile = await _fileService.UploadAsync(post.FileUpload);
        string? fileUploadName = blobFile.Blob.Uri;
        post.CreatedAt = DateTime.Now;
        post.LastModified = DateTime.Now;
        post.FilePath = fileUploadName;
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task DeletePost(int id)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == id);
        string fileName = Path.GetFileNameWithoutExtension(post.FilePath);
        if (post is null) return;
        await _fileService.DeleteAsync(fileName);
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

    public async Task<Post> UpdatePost(IFormFile? UpdateFile, string PostContent, int id)
    {
        var post = _context.Posts.FirstOrDefault(x => x.Id == id);
        var fileName = Path.GetFileNameWithoutExtension(post.FilePath);
        post.LastModified = DateTime.Now;
        post.Content = PostContent;
        if (fileName != UpdateFile?.FileName)
        {
            await _fileService.DeleteAsync(fileName);
            var blobFile = await _fileService.UploadAsync(UpdateFile);
            string? newFilePath = blobFile.Blob.Uri;
            post.FilePath = newFilePath;
        }

        await _context.SaveChangesAsync();
        return post;
    }
}