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
        CreateMap<PostDto, CreatePost.Command>();
        CreateMap<CreatePost.Command, CreatePostDto>();
        CreateMap<CreatePostDto, Post>()
            .AfterMap(
                (source, des) =>
                {
                    des.Slug = source.Title?.Trim().Replace(" ", "-");
                }
            );
        CreateMap<PostDto, EditPost.Command>();
        CreateMap<EditPost.Command, EditPostDto>();
        CreateMap<EditPostDto, Post>();
    }
}
