using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PostModel, IRequest<Post>>();
        CreateMap<RegisterUserDto, IRequest<User>>();
        CreateMap<IRequest<User>, IdentityUser>();
    }
}
