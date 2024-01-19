using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Comments;
using MediatR;

namespace Application.Comments.Command;

public class CreateComment
{
    public class Command : IRequest
    {
        public string? Content { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Command, CreateCommentDto>(request);
            await _repository.CreateComment(comment);
        }
    }
}
