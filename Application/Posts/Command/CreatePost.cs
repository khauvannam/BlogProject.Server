using Domain.Entity.Post;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Command;

public class CreatePost : IRequest<Post>
{
    public string? Title { get; init; }
    public string? PostContent { get; init; }
    public IFormFile? FileUpload { get; init; }
}