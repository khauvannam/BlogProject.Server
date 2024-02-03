using Domain.Entity.Tags;

namespace Application.Abstraction;

public interface ITagRepository
{
    //TODO ADD RESULT PATTERN TO TAG, USER, TOKEN
    Task CreateTag(string tagName);
    Task EditTag(string id, string tagName);
    Task DeleteTag(string id);
    Task<ICollection<Tag>> GetAllTag();
}
