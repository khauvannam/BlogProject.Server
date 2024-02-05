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

    public class Handler(IPostRepository postRepo, IMapper mapper) : IRequestHandler<Command, Result<Post>>
    {
        public async Task<Result<Post>> Handle(Command request, CancellationToken cancellationToken)
        {
            var post = mapper.Map<Command, CreatePostDto>(request);

            var result = await postRepo.CreatePost(post);
            return result;
        }
    }
}
