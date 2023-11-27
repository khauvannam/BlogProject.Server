using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;

namespace Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, Register>().ReverseMap();
        CreateMap<RegisterUserDto, User>();
    }
}
