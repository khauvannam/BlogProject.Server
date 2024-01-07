using Application.Posts.Command;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;
using Domain.Entity.Posts;

namespace Application.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePost.Command, PostDto>().ReverseMap();
        CreateMap<CreatePostDto, Post>();
        CreateMap<CreatePost.Command, CreatePostDto>();
        CreateMap<EditPostDto, PostDto>();
        CreateMap<EditPostDto, Post>();
    }
}
