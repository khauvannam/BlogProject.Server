using Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Domain.Entity.Post;

public class EditPostDto : PostModel<Guid>
{
    public IFormFile FileUpload { get; set; }
}
