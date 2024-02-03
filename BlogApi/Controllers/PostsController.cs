using Application.Posts.Command;
using Application.Posts.Queries;
using AutoMapper;
using Domain.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(policy: "Users")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PostsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost($"{nameof(CreatePost)}")]
    public async Task<IActionResult> CreatePost([FromForm] PostDto postDto)
    {
        var createPost = _mapper.Map<PostDto, CreatePost.Command>(postDto);
        var result = await _mediator.Send(createPost);
        return CreatedAtRoute(nameof(GetPostById), new { result.Value!.Id }, result.Value);
    }

    [HttpGet("{id}", Name = nameof(GetPostById)), AllowAnonymous]
    public async Task<IActionResult> GetPostById(string id)
    {
        var post = new GetPostById.Command { Id = id };
        var result = await _mediator.Send(post);
        return Ok(result.Value);
    }

    [HttpGet($"/{nameof(GetAllPost)}"), AllowAnonymous]
    public async Task<IActionResult> GetAllPost()
    {
        var posts = new GetAllPosts.Command();
        var result = await _mediator.Send(posts);
        return Ok(result.Value);
    }

    [HttpDelete($"{nameof(DeletePost)}/{{id}}")]
    public async Task<IActionResult> DeletePost(string id)
    {
        var post = new DeletePost.Command { Id = id };
        var result = await _mediator.Send(post);
        return Ok($"Post with id {result.Value} have been removed");
    }

    [HttpPut($"{nameof(UpdatePostById)}/{{id}}")]
    public async Task<IActionResult> UpdatePostById(string id, [FromForm] PostDto postDto)
    {
        var post = _mapper.Map<PostDto, EditPost.Command>(postDto);
        post.Id = id;
        var result = await _mediator.Send(post);
        return Ok(result.Value);
    }

    [HttpGet("/search/{tags}"), AllowAnonymous]
    public async Task<IActionResult> GetAllPostByTags(string tags)
    {
        var listTags = tags.Split("+").ToList();
        var postsCommand = new GetAllPostByTags.Command { ListTags = listTags };
        var posts = await _mediator.Send(postsCommand);
        return Ok(posts.Value);
    }
}
