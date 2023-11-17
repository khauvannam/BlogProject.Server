using Microsoft.AspNetCore.Http;

namespace Domain.Abstraction;

public abstract class PostDto : APost
{
    public IFormFile? FileUpload { get; init; }
}
