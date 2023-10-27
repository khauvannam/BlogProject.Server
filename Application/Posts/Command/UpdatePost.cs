using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Command;

public class UpdatePost : IRequest<Post>
{
    public int Id { get; set; }
    public string PostContent { get; set; }
    public IFormFile? UpdateFile { get; set; }
}