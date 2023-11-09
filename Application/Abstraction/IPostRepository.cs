using Domain.Entity.Post;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Abstraction;

public interface IPostRepository
{
    Task<ICollection<Post>> GetAllPosts();
    Task<Post> GetsPostById(Guid id);
    Task<Post> CreatePost(CreatePostDto createPost);
    Task DeletePost(Guid id);
    Task<Post> UpdatePost(EditPostDto editPostDto);
}