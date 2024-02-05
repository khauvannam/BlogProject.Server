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
public class PostsController(ISender mediator, IMapper mapper) : ControllerBase
{
    [HttpPost($"{nameof(CreatePost)}")]
    public async Task<IActionResult> CreatePost([FromForm] PostDto postDto)
    {
        var createPost = mapper.Map<PostDto, CreatePost.Command>(postDto);
        var result = await mediator.Send(createPost);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtRoute(nameof(GetPostById), new { result.Value!.Id }, result.Value);
    }

    [HttpGet("{id}", Name = nameof(GetPostById)), AllowAnonymous]
    public async Task<IActionResult> GetPostById(string id)
    {
        var post = new GetPostById.Command { Id = id };
        var result = await mediator.Send(post);
        return result.IsFailure ? BadRequest(result.Errors) : Ok(result.Value);
    }

    [HttpGet($"/{nameof(GetAllPost)}"), AllowAnonymous]
    public async Task<IActionResult> GetAllPost()
    {
        var posts = new GetAllPosts.Command();
        var result = await mediator.Send(posts);

        return result.IsFailure ? BadRequest(result.Errors) : Ok(result.Value);
    }

    [HttpDelete($"{nameof(DeletePost)}/{{id}}")]
    public async Task<IActionResult> DeletePost(string id)
    {
        var post = new DeletePost.Command { Id = id };
        var result = await mediator.Send(post);
        return result.IsFailure
            ? BadRequest(result.Errors)
            : Ok($"Post with id {result.Value} have been removed");
    }

    [HttpPut($"{nameof(UpdatePostById)}/{{id}}")]
    public async Task<IActionResult> UpdatePostById(string id, [FromForm] PostDto postDto)
    {
        var post = mapper.Map<PostDto, EditPost.Command>(postDto);
        post.Id = id;
        var result = await mediator.Send(post);
        return result.IsFailure ? BadRequest(result.Errors) : Ok(result.Value);
    }

    [HttpGet("/search/{tags}"), AllowAnonymous]
    public async Task<IActionResult> GetAllPostByTags(string tags)
    {
        var listTags = tags.Split("+").ToList();
        var posts = new GetAllPostByTags.Command { ListTags = listTags };
        var result = await mediator.Send(posts);
        return result.IsFailure ? BadRequest(result.Errors) : Ok(result.Value);
    }
}
