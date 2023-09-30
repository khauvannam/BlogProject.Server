using Domain.Models;

namespace BlogProject.Server.Application.Abstraction
{
    public interface IPostRepository
    {
        Task<ICollection<Post>> GetAllPosts();
        Task<Post> GetsPostById(int id);
        Task<Post> CreatePost(Post post);
        Task DeletePost(int id);
        Task<Post> UpdatePost(string PostContent, int id);

    }
}
