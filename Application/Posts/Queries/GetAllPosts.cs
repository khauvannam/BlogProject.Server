using Application.Abstraction;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Queries;

public class GetAllPosts
{
    public class Command : IRequest<ICollection<Post>> { }

    public class Handler : IRequestHandler<Command, ICollection<Post>>
    {
        private readonly IPostRepository _postRepo;

        public Handler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<ICollection<Post>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            return await _postRepo.GetAllPosts();
        }
    }
}
