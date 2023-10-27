using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public string? FilePath { get; set; }
    public Guid UserId { get; set; }
    [NotMapped] [JsonIgnore] public IFormFile? FileUpload { get; set; }
}