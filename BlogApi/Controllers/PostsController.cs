using Application.Posts.Command;
using Application.Posts.Queries;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

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
    public async Task<IActionResult> CreatePost([FromForm] PostDto postDto)
    {
        var createPost = _mapper.Map<PostDto, CreatePost.Command>(postDto);
        var newPost = await _mediator.Send(createPost);
        return CreatedAtRoute(nameof(GetPostById), new { newPost.Id }, newPost);
    }

    [HttpGet("{id:guid}", Name = "GetPostById")]
    public async Task<IActionResult> GetPostById(string id)
    {
        var post = new GetPostById.Command { Id = id };
        var gotPost = await _mediator.Send(post);
        return Ok(gotPost);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPost()
    {
        var allPost = new GetAllPosts.Command();
        var newAllPost = await _mediator.Send(allPost);
        return Ok(newAllPost);
    }

    [HttpDelete("{id:alpha}")]
    public async Task<IActionResult> DeletePost(string id)
    {
        var post = new DeletePost.Command { Id = id };
        await _mediator.Send(post);
        return NoContent();
    }

    [HttpPut("{id:alpha}")]
    public async Task<IActionResult> UpdatePostById(string id, [FromForm] PostDto postDto)
    {
        var post = _mapper.Map<PostDto, EditPost.Command>(postDto);
        var editedPost = await _mediator.Send(post);
        return Ok(editedPost);
    }
}
