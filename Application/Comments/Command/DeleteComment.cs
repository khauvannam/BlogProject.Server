using Application.Abstraction;
using MediatR;

namespace Application.Comments.Command;

public class DeleteComment
{
    public class Command : IRequest
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly ICommentRepository _repository;

        public Handler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _repository.DeleteComment(request.Id);
        }
    }
}
