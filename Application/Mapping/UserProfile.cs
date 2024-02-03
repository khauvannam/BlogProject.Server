using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;
using Domain.Entity.Users;

namespace Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, RegisterUser.Command>().ReverseMap();
        CreateMap<RegisterDto, User>();
        CreateMap<LoginDto, LoginUser.Command>().ReverseMap();
    }
}
