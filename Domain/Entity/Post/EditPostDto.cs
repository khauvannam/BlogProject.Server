using Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Domain.Entity.Post;

public class EditPostDto : PostDto
{
    public string Id { get; init; }
}
