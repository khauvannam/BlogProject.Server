using Application.Posts.Command;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;

namespace Application.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePost, CreatePostDto>().ReverseMap();
        CreateMap<CreatePostDto, PostModel<string>>();
        CreateMap<EditPostDto, PostModel<string>>();
    }
}
