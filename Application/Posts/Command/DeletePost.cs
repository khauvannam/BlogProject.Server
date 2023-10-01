using Domain.Models;
using MediatR;

namespace Application.Posts.Command
{
    public class DeletePost : IRequest
    {
        public int Id { get; set; }
    }
}
