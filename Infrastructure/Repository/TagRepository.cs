using Application.Abstraction;
using Domain.Entity.Tags;

namespace Infrastructure.Repository;

public class TagRepository : ITagRepository
{
    private readonly UserDbContext _dbContext;

    public TagRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateTag(string tagName)
    {
        var tag = new Tag { TagName = tagName };
        _dbContext.Tags.Add(tag);
        await _dbContext.SaveChangesAsync();
    }

    public Task EditTag(string id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTag(string id)
    {
        throw new NotImplementedException();
    }

    public Task GetAllTag()
    {
        throw new NotImplementedException();
    }
}
