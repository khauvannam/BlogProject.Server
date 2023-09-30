using Domain.Models;
using MediatR;

namespace BlogProject.Server.Application.Posts.Queries
{
    public class GetPostById : IRequest<Post>
    {
        public int Id { get; set; } 
    }
}
