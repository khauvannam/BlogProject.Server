using Domain.Entity.Post;
using Domain.Models;
using MediatR;

namespace Application.Posts.Queries;

public class GetPostById : IRequest<Post>
{
    public string Id { get; init; }
}
