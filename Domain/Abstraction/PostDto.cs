using Microsoft.AspNetCore.Http;

namespace Domain.Abstraction;

public class PostDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool Public { get; set; } = false;
    public IFormFile? FileUpload { get; init; }
    public List<string>? TagId { get; init; }
}
