using Domain.Models;
using MediatR;

namespace BlogProject.Server.Application.Posts.Queries
{
    public class GetAllPosts : IRequest<ICollection<Post>>
    {
    }
}
