using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Comments;
using MediatR;

namespace Application.Comments.Command;

public class EditComment
{
    public class Command : IRequest
    {
        public string? Id { get; set; }
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
            var comment = _mapper.Map<Command, EditCommentDto>(request);
            await _repository.EditComment(comment);
        }
    }
}
