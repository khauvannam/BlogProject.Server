using Application.Abstraction;
using Application.Posts.Command;
using Domain.Entity.Post;
using MediatR;

namespace Application.Posts.CommandHandlers;

public class EditPostHandler : IRequestHandler<EditPost, Post>
{
    private readonly IPostRepository _postRepo;

    public EditPostHandler(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<Post> Handle(EditPost request, CancellationToken cancellationToken)
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
