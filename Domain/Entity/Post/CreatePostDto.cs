using Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Domain.Entity.Post;

public class CreatePostDto : PostModel<Guid>
{
    public IFormFile? FileUpload { get; init; }
}
