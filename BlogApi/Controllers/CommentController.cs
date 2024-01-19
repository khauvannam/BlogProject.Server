using Application.Comments.Command;
using AutoMapper;
using Domain.Entity.Comments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CommentController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost, Authorize(Policy = "Users")]
    public async Task<IActionResult> CreateComment(CommentDto commentDto)
    {
        var comment = _mapper.Map<CommentDto, CreateComment.Command>(commentDto);
        await _mediator.Send(comment);
        return Ok("Your comment is created");
    }

    [HttpDelete("{id}"), Authorize(Policy = "Users")]
    public async Task<IActionResult> DeleteComment(string id)
    {
        var comment = new DeleteComment.Command { Id = id };
        await _mediator.Send(comment);
        return NoContent();
    }

    [HttpPut("{id}"), Authorize(Policy = "Users")]
    public async Task<IActionResult> EditComment(string id, CommentDto commentDto)
    {
        var comment = _mapper.Map<CommentDto, EditComment.Command>(commentDto);
        comment.Id = id;
        await _mediator.Send(comment);
        return Ok();
    }
}
