using Application.Abstraction;
using Application.Posts.Command;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers;

public class UpdatePostHandler : IRequestHandler<UpdatePost, Post>
{
    private readonly IPostRepository _postRepo;

    public UpdatePostHandler(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
    {
        var post = await _postRepo.UpdatePost(request.PostContent, request.Id);
        return post;
    }
}