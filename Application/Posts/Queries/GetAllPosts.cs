using Application.Abstraction;
using Application.Error;
using Domain.Entity.ErrorsHandler;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Queries;

public class GetAllPosts
{
    public class Command : IRequest<Result<ICollection<Post>>> { }

    public class Handler(IPostRepository postRepo) : IRequestHandler<Command, Result<ICollection<Post>>>
    {
        public async Task<Result<ICollection<Post>>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var result = await postRepo.GetAllPosts();
            return result;
        }
    }
}
