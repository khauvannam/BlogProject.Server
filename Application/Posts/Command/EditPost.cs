using Application.Abstraction;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Command;

public class EditPost
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
            var updatedPost = _mapper.Map<Command, EditPostDTO>(request);
            var post = await _postRepo.UpdatePost(updatedPost);
            return post;
        }
    }
}
