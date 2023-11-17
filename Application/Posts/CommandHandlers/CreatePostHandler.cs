using Application.Abstraction;
using Application.Posts.Command;
using Domain.Entity.Post;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers;

public class CreatePostHandler : IRequestHandler<CreatePost, Post>
{
    private readonly IPostRepository _postRepo;

    public CreatePostHandler(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        var post = new CreatePostDto
        {
            Title = request.Title,
            Content = request.Content,
            FileUpload = request.FileUpload
        };

        return await _postRepo.CreatePost(post);
    }
}
