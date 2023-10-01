using Application.Abstraction;
using Application.Posts.Command;
using MediatR;

namespace Application.Posts.CommandHandlers
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
