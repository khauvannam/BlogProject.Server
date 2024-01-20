using Application.Tags.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route(("/[controller]"))]
[ApiController]
[Authorize(Policy = "Admin")]
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
        var tagsCommand = new GetAllTags.Command;
        var tags = await _mediator.Send(tagsCommand);
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag(string tagName) { }

    [HttpDelete("{tagId}")]
    public async Task<IActionResult> DeleteTag(string tagId) { }

    [HttpPut("{tagId}")]
    public async Task<IActionResult> EditTag(string tagId) { }
}
