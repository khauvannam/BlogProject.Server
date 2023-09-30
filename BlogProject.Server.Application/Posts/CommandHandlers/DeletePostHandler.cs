using BlogProject.Server.Application.Abstraction;
using BlogProject.Server.Application.Posts.Command;
using MediatR;

namespace BlogProject.Server.Application.Posts.CommandHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePost>
    {
        private readonly IPostRepository _postRepo;
        public DeletePostHandler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }
        public async Task Handle(DeletePost request, CancellationToken cancellationToken)
        {
            await _postRepo.DeletePost(request.Id);
        }
    }
}
