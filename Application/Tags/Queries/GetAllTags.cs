using Application.Abstraction;
using Domain.Entity.Tags;
using MediatR;

namespace Application.Tags.Queries;

public class GetAllTags
{
    public class Command : IRequest<ICollection<Tag>> { }

    public class Handler : IRequestHandler<Command, ICollection<Tag>>
    {
        private readonly ITagRepository _repository;

        public Handler(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Tag>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAllTag();
        }
    }
}
