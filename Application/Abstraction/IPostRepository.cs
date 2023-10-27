using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Abstraction;

public interface IPostRepository
{
    Task<ICollection<Post>> GetAllPosts();
    Task<Post> GetsPostById(int id);
    Task<Post> CreatePost(Post post);
    Task DeletePost(int id);
    Task<Post> UpdatePost(IFormFile? UpdateFile, string PostContent, int id);
}