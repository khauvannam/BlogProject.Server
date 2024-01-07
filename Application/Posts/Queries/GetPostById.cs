using Application.Abstraction;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Queries;

public class GetPostById
{
    public class Command : IRequest<Post>
    {
        public string Id { get; init; }
    }

    public class Handler : IRequestHandler<Command, Post>
    {
        private readonly IPostRepository _postRepo;

        public Handler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Post> Handle(Command request, CancellationToken cancellationToken)
        {
            var post = await _postRepo.GetsPostById(request.Id);
            return post;
        }
    }
}
