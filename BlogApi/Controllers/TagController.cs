using Application.Tags.Command;
using Application.Tags.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route(("/[controller]"))]
[ApiController]
[Authorize(policy: "Admin")]
public class TagController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTag()
    {
        var tagsCommand = new GetAllTags.Command();
        var tags = await mediator.Send(tagsCommand);
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag(string tagName)
    {
        var tag = new CreateTag.Command { Content = tagName };
        await mediator.Send(tag);
        return Ok();
    }

    [HttpDelete("{tagId}")]
    public async Task<IActionResult> DeleteTag(string tagId)
    {
        var tag = new DeleteTag.Command { TagId = tagId };
        await mediator.Send(tag);
        return NoContent();
    }

    [HttpPut("{tagId}")]
    public async Task<IActionResult> EditTag(string tagId, string tagName)
    {
        var tag = new EditTag.Command { TagId = tagId, TagName = tagName };
        await mediator.Send(tag);
        return Ok();
    }
}
