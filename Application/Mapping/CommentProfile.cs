using Application.Comments.Command;
using AutoMapper;
using Domain.Entity.Comments;

namespace Application.Mapping;

public class CommentProfile : Profile
{
    protected CommentProfile()
    {
        CreateMap<CommentDto, CreateComment.Command>();
        CreateMap<CreateComment.Command, CreateCommentDto>();
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<CommentDto, EditComment.Command>();
        CreateMap<EditComment.Command, EditCommentDto>();
        CreateMap<EditCommentDto, Comment>();
    }
}
