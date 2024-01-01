using Application.Posts.Command;
using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Post;

namespace Application.Mapping;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePost.Command, CreatePostDTO>().ReverseMap();
        CreateMap<CreatePostDTO, Post>();
        CreateMap<CreatePostDTO, PostDTO>();
        CreateMap<EditPostDTO, PostDTO>();
        CreateMap<EditPostDTO, Post>();
    }
}
