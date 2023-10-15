using Application.Abstraction;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;


namespace Application.Posts.QueriesHandlers;

public class GetAllPostsHandler : IRequestHandler<GetAllPosts, ICollection<Post>>
{
    private readonly IPostRepository _postRepo;

    public GetAllPostsHandler(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<ICollection<Post>> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        return await _postRepo.GetAllPosts();
    }
}