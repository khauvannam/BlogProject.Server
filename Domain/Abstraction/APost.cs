using Microsoft.AspNetCore.Http;

namespace Domain.Abstraction;

public class APost
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool Public { get; set; } = false;
}
