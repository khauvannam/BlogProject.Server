using Domain.Entity.Post;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Command;

public class UpdatePost : IRequest<Post>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? PostContent { get; set; }
    public IFormFile? FileUpload { get; set; }
}