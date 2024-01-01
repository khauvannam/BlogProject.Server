using Application.Abstraction;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;
using MediatR;

namespace Application.Posts.Command;

public static class CreatePost
{
    public class Command : PostDTO, IRequest<Post> { }

    public class Handler : IRequestHandler<Command, Post>
    {
        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;

        public Handler(IPostRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<Post> Handle(Command request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Command, CreatePostDTO>(request);

            return await _postRepo.CreatePost(post);
        }
    }
}
