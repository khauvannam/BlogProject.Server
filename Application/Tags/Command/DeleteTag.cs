using Application.Abstraction;
using MediatR;

namespace Application.Tags.Command;

public class DeleteTag
{
    public class Command : IRequest
    {
        public string TagId { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly ITagRepository _tagRepository;

        public Handler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _tagRepository.DeleteTag(request.TagId);
        }
    }
}
