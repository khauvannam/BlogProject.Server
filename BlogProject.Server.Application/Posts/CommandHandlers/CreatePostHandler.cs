using BlogProject.Server.Application.Abstraction;
using BlogProject.Server.Application.Posts.Command;
using Domain.Models;
using MediatR;

namespace BlogProject.Server.Application.Posts.CommandHandlers
{
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
            string? filePath = blobFile.Blob.Uri;
            var post = new Post
            {
                Content = request.PostContent,
                FilePath = filePath
            };

            return await _postRepo.CreatePost(post);
        }
    }
}