using Domain.Models;
using MediatR;

namespace BlogProject.Server.Application.Posts.Command
{
    public class DeletePost : IRequest
    {
        public int Id { get; set; }
    }
}
