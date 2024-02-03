using Application.Tags.Command;
using Application.Tags.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route(("/[controller]"))]
[ApiController]
[Authorize(policy: "Admin")]
public class TagController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTag()
    {
        var tagsCommand = new GetAllTags.Command();
        var tags = await _mediator.Send(tagsCommand);
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag(string tagName)
    {
        var tag = new CreateTag.Command { Content = tagName };
        await _mediator.Send(tag);
        return Ok();
    }

    [HttpDelete("{tagId}")]
    public async Task<IActionResult> DeleteTag(string tagId)
    {
        var tag = new DeleteTag.Command { TagId = tagId };
        await _mediator.Send(tag);
        return NoContent();
    }

    [HttpPut("{tagId}")]
    public async Task<IActionResult> EditTag(string tagId, string tagName)
    {
        var tag = new EditTag.Command { TagId = tagId, TagName = tagName };
        await _mediator.Send(tag);
        return Ok();
    }
}
