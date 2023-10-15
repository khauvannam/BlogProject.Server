using Application.Posts.Command;
using Application.Posts.Queries;
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
        return CreatedAtRoute("GetPostById", new { post.Id }, newPost);
    }

    [HttpGet("{id:int}", Name = "GetPostById")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = new GetPostById { Id = id };
        var gotPost = await _mediator.Send(post);
        return Ok(gotPost);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPost()
    {
        var allPost = new GetAllPosts();
        var newAllPost = await _mediator.Send(allPost);
        return Ok(newAllPost);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = new DeletePost { Id = id };
        await _mediator.Send(post);
        return NoContent();
    }
}