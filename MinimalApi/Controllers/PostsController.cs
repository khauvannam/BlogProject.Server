using Application.Posts.Command;
using Application.Posts.Queries;
using AutoMapper;
using Domain.Entity.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PostsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
    {
        var createPost = _mapper.Map<CreatePostDto, CreatePost>(createPostDto);
        var newPost = await _mediator.Send(createPost);
        return CreatedAtRoute(nameof(GetPostById), new { newPost.Id }, newPost);
    }

    [HttpGet("{id:guid}", Name = "GetPostById")]
    public async Task<IActionResult> GetPostById(string id)
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

    [HttpDelete("{id:alpha}")]
    public async Task<IActionResult> DeletePost(string id)
    {
        var post = new DeletePost { Id = id };
        await _mediator.Send(post);
        return NoContent();
    }

    [HttpPut("{id:alpha}")]
    public async Task<IActionResult> UpdatePostById(string id, [FromForm] EditPostDto editPostDto)
    {
        return Ok("Post updated successfully");
    }
}
