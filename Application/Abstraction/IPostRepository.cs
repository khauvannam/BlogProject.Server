using Domain.Entity.Post;
using Domain.Entity.Posts;
using Microsoft.AspNetCore.Http;

namespace Application.Abstraction;

public interface IPostRepository
{
    Task<ICollection<Post>> GetAllPosts();
    Task<Post> GetsPostById(string id);
    Task<Post> CreatePost(CreatePostDto createPostDto);
    Task DeletePost(string id);
    Task<Post> UpdatePost(EditPostDto editPostDto);
    Task<ICollection<Post>> GetAllPostByTag(string tagName);
}
