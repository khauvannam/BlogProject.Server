using MediatR;

namespace Application.Posts.Command;

public class DeletePost : IRequest
{
    public string Id { get; init; }
}