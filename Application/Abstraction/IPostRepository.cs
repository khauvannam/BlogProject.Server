using Domain.Entity.Post;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Abstraction;

public interface IPostRepository
{
    Task<ICollection<Post>> GetAllPosts();
    Task<Post> GetsPostById(string id);
    Task<Post> CreatePost(CreatePostDTO createPostDto);
    Task DeletePost(string id);
    Task<Post> UpdatePost(EditPostDTO editPostDto);
}
