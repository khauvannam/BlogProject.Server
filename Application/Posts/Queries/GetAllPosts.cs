using Application.Abstraction;
using Application.Error;
using Domain.Entity.ErrorsHandler;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Queries;

public class GetAllPosts
{
    public class Command : IRequest<Result<ICollection<Post>>> { }

    public class Handler : IRequestHandler<Command, Result<ICollection<Post>>>
    {
        private readonly IPostRepository _postRepo;

        public Handler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Result<ICollection<Post>>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var result = await _postRepo.GetAllPosts();
            return result.IsFailure ? PostErrors.NotFoundAnyPost : result;
        }
    }
}
