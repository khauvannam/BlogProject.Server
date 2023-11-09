using Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Domain.Entity.Post;

public class CreatePostDto : PostModel
{
    public IFormFile? FileUpload { get; init; }
}