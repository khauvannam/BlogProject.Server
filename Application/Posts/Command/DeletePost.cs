using Application.Abstraction;
using Application.Error;
using MediatR;

namespace Application.Posts.Command;

public class DeletePost
{
    public class Command : IRequest<Result<string>>
    {
        public string Id { get; init; }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IPostRepository _postRepo;

        public Handler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var result = await _postRepo.DeletePost(request.Id);
            return result.IsFailure ? result.Errors : result;
        }
    }
}
