using Domain.Entity.PostsTags;

namespace Domain.Entity.Tags;

public class Tag
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TagName { get; set; }
    public List<PostTag> PostTags { get; set; }
}
