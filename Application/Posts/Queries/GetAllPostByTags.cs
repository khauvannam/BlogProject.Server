using Application.Abstraction;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Queries;

public sealed class GetAllPostByTags
{
    public class Command : IRequest<ICollection<Post>>
    {
        public List<string>? TagIds { get; set; }
    }

    public class Handler : IRequestHandler<Command, ICollection<Post>>
    {
        private readonly IPostRepository _repository;

        public Handler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Post>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllPostByTags(request.TagIds);
        }
    }
}
