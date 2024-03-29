﻿using AutoMapper;
using Domain.Abstraction;
using Domain.Entity.Comments;

namespace Application.Mapping;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentDto, Comment>();
        CreateMap<EditCommentDto, Comment>();
    }
}
