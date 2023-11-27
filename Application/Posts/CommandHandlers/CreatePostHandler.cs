using Application.Abstraction;
using Application.Posts.Command;
using AutoMapper;
using Domain.Entity.Post;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers;

public class CreatePostHandler : IRequestHandler<CreatePost, Post>
{
    private readonly IPostRepository _postRepo;
    private readonly IMapper _mapper;

    public CreatePostHandler(IPostRepository postRepo, IMapper mapper)
    {
        _postRepo = postRepo;
        _mapper = mapper;
    }

    public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<CreatePost, CreatePostDto>(request);

        return await _postRepo.CreatePost(post);
    }
}
