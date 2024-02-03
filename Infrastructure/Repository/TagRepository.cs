using Application.Abstraction;
using Domain.Entity.Tags;
using Microsoft.EntityFrameworkCore;

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

    public async Task EditTag(string id, string tagName)
    {
        var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == id);
        if (tag is null)
            throw new Exception("Can not find your tag id");
        tag.TagName = tagName;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTag(string id)
    {
        var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == id);
        _dbContext.Tags.Remove(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Tag>> GetAllTag()
    {
        var tags = await _dbContext.Tags.ToListAsync();
        return tags;
    }
}
