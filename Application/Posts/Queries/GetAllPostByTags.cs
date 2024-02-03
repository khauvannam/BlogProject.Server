using Application.Abstraction;
using Application.Error;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Queries;

public sealed class GetAllPostByTags
{
    public class Command : IRequest<Result<ICollection<Post>>>
    {
        public List<string>? ListTags { get; init; }
    }

    public class Handler : IRequestHandler<Command, Result<ICollection<Post>>>
    {
        private readonly IPostRepository _repository;

        public Handler(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ICollection<Post>>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var result = await _repository.GetAllPostByTags(request.ListTags);
            return result;
        }
    }
}
