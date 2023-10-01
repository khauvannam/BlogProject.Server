using Application.Abstraction;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _context;
        public PostRepository(SocialDbContext context)
        {
            _context = context;
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
            if (post is null) return;
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
}
