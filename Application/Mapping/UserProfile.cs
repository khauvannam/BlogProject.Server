using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;

namespace Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDTO, Register>().ReverseMap();
        CreateMap<RegisterDTO, User>();
    }
}
