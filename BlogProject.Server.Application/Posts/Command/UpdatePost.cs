using Domain.Models;
using MediatR;

namespace BlogProject.Server.Application.Posts.Command
{
    public class UpdatePost : IRequest<Post>
    {
        public int Id { get; set; }
        public string? PostContent { get; set; }
    }
}
