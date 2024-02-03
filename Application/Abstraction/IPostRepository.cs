using Application.Error;
using Domain.Entity.Post;
using Domain.Entity.Posts;

namespace Application.Abstraction;

public interface IPostRepository
{
    Task<Result<ICollection<Post>>> GetAllPosts();
    Task<Result<Post>> GetsPostById(string id);
    Task<Result<Post>> CreatePost(CreatePostDto createPostDto);
    Task<Result<string>> DeletePost(string id);
    Task<Result<Post>> EditPost(EditPostDto editPostDto);
    Task<Result<ICollection<Post>>> GetAllPostByTags(List<string>? tags);
}
