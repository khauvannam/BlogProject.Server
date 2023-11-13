using System.ComponentModel.DataAnnotations;

namespace Domain.Abstraction;

public abstract class PostModel<T>
{
    [Key]
    public T Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}
