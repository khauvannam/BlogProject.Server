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

    public class Handler(IPostRepository postRepo) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var result = await postRepo.DeletePost(request.Id);
            return result;
        }
    }
}
