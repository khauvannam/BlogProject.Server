using Application.Posts.Command;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace MinimalApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm] Post post)
    {
        var createPost = new CreatePost
            { PostContent = post.Content, FileUpload = post.FileUpload, Title = post.Title };
        var newPost = await _mediator.Send(createPost);
        return Ok(newPost);
    }
}