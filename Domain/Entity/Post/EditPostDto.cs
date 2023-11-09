using Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Domain.Entity.Post;

public class EditPostDto : PostModel
{
    public Guid Id { get; set; }
    public IFormFile FileUpload { get; set; }
}