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
        _fileService = fileService;
    }

    public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        var blobFile = await _fileService.UploadAsync(request.FileUpload);
        var filePath = blobFile.Blob.Uri;
        var post = new Post
        {
            Title = request.Title,
            Content = request.PostContent,
            FilePath = filePath
        };

        return await _postRepo.CreatePost(post);
    }
}