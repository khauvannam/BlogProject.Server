using Application.Abstraction;
using MediatR;

namespace Application.Posts.Command;

public class DeletePost
{
    public class Command : IRequest
    {
        public string Id { get; init; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IPostRepository _postRepo;

        public Handler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _postRepo.DeletePost(request.Id);
        }
    }
}
