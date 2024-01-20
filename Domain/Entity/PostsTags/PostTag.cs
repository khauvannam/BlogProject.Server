using Domain.Entity.Tags;

namespace Domain.Entity.PostsTags;

public class PostTag
{
    public Posts.Post Post { get; set; }
    public string PostId { get; set; }
    public Tag Tag { get; set; }
    public string TagId { get; set; }
}
