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
        CreateMap<PostModel<string>, IRequest<Post>>().ReverseMap();
        CreateMap<PostDto, PostModel<Guid>>();
        CreateMap<RegisterUserDto, IRequest<User>>().ReverseMap();
        CreateMap<RegisterUserDto, IdentityUser<string>>();
    }
}
