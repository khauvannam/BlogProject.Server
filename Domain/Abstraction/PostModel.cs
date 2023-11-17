using System.ComponentModel.DataAnnotations;

namespace Domain.Abstraction;

public abstract class PostModel<TKey> : APost
{
    public TKey Id { get; set; }
}
