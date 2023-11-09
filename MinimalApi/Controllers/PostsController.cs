using Application.Posts.Command;
using Application.Posts.Queries;
using Domain.Entity.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
    {
        var createPost = new CreatePost
            { PostContent = createPostDto.Content, FileUpload = createPostDto.FileUpload, Title = createPostDto.Title };
        var newPost = await _mediator.Send(createPost);
        return CreatedAtRoute("GetPostById", new { newPost.Id }, newPost);
    }

    [HttpGet("{id:guid}", Name = "GetPostById")]
    public async Task<IActionResult> GetPostById(Guid id)
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var post = new DeletePost { Id = id };
        await _mediator.Send(post);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePostById([FromForm] EditPostDto editPostDto)
    {
        return Ok("Post updated successfully");
    }
}