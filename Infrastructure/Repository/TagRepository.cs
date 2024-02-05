using Application.Abstraction;
using Domain.Entity.Tags;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TagRepository(UserDbContext dbContext) : ITagRepository
{
    public async Task CreateTag(string tagName)
    {
        var tag = new Tag { TagName = tagName };
        dbContext.Tags.Add(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task EditTag(string id, string tagName)
    {
        var tag = dbContext.Tags.FirstOrDefault(t => t.Id == id);
        if (tag is null)
            throw new Exception("Can not find your tag id");
        tag.TagName = tagName;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteTag(string id)
    {
        var tag = dbContext.Tags.FirstOrDefault(t => t.Id == id);
        dbContext.Tags.Remove(tag);
        await dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Tag>> GetAllTag()
    {
        var tags = await dbContext.Tags.ToListAsync();
        return tags;
    }
}
