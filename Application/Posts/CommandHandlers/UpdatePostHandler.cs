using Application.Abstraction;
using Application.Posts.Command;
using Domain.Entity.Post;
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
        var updatedPost = new EditPostDto
        {
            Content = request.Content,
            FileUpload = request.FileUpload,
            Title = request.Title
        };
        var post = await _postRepo.UpdatePost(updatedPost);
        return post;
    }
}
