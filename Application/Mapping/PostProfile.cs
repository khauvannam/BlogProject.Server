using Application.Posts.Command;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;

namespace Application.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePost.Command, CreatePostDto>().ReverseMap();
        CreateMap<CreatePostDto, Post>();
        CreateMap<CreatePostDto, PostDto>();
        CreateMap<EditPostDto, PostDto>();
        CreateMap<EditPostDto, Post>();
    }
}
