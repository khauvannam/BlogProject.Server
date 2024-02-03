using Application.Abstraction;
using Application.Error;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Queries;

public class GetPostById
{
    public class Command : IRequest<Result<Post>>
    {
        public string Id { get; init; }
    }

    public class Handler : IRequestHandler<Command, Result<Post>>
    {
        private readonly IPostRepository _postRepo;

        public Handler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Result<Post>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _postRepo.GetsPostById(request.Id);
            return result.IsFailure ? result.Errors : result;
        }
    }
}
