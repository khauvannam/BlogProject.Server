using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;

namespace Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, RegisterUser.Command>().ReverseMap();
        CreateMap<RegisterDto, User>();
    }
}
