using Domain.Abstraction;
using Domain.Entity.Post;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Command;

public class EditPost : PostDto, IRequest<Post> { }
