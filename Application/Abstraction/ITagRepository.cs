using Application.Tags.Command;

namespace Application.Abstraction;

public interface ITagRepository
{
    Task CreateTag(string tagName);
    Task EditTag(string id);
    Task DeleteTag(string id);
    Task GetAllTag();
}
