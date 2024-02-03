using Application.Abstraction;
using Application.Error;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;
using Domain.Entity.Posts;
using MediatR;

namespace Application.Posts.Command;

public static class CreatePost
{
    public class Command : PostDto, IRequest<Result<Post>> { }

    public class Handler : IRequestHandler<Command, Result<Post>>
    {
        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;

        public Handler(IPostRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<Result<Post>> Handle(Command request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Command, CreatePostDto>(request);

            var result = await _postRepo.CreatePost(post);
            return result.IsFailure ? result.Errors : result;
        }
    }
}
