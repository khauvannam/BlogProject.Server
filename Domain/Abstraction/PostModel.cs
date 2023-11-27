using System.ComponentModel.DataAnnotations;

namespace Domain.Abstraction;

public abstract class PostModel<TKey> : APost
{
    public TKey? Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; } = DateTime.Now;
}
