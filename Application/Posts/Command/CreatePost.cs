using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Command
{
    public class CreatePost : IRequest<Post>
    {
        public string? PostContent { get; set; }
        public IFormFile? FileUpload { get; set; }
    }
}