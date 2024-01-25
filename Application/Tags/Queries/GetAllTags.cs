using Domain.Entity.Tags;
using MediatR;

namespace Application.Tags.Queries;

public class GetAllTags
{
    public class Command : IRequest<ICollection<Tag>> { }

    public class Handler : IRequestHandler<Command, ICollection<Tag>>
    {
        public Task<ICollection<Tag>> Handle(Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
