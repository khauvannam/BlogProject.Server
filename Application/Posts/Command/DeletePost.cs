using MediatR;

namespace Application.Posts.Command;

public class DeletePost : IRequest
{
    public Guid Id { get; init; }
}