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

    public class Handler(IPostRepository repository) : IRequestHandler<Command, Result<ICollection<Post>>>
    {
        public async Task<Result<ICollection<Post>>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var result = await repository.GetAllPostByTags(request.ListTags);
            return result;
        }
    }
}
