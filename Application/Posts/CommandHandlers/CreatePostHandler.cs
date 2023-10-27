using Application.Abstraction;
using Application.Posts.Command;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers;

public class CreatePostHandler : IRequestHandler<CreatePost, Post>
{
    private readonly IPostRepository _postRepo;
    private readonly IFileService _fileService;

    public CreatePostHandler(IPostRepository postRepo, IFileService fileService)
    {
        _postRepo = postRepo;
    }

    public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        
        var post = new Post
        {
            Title = request.Title,
            Content = request.PostContent,
            FileUpload = request.FileUpload
        };

        return await _postRepo.CreatePost(post);
    }
}