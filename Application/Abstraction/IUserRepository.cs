using Domain.Entity.Auth;
using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Entity.Users;

namespace Application.Abstraction;

public interface IUserRepository
{
    public Task Register(RegisterDto user);
    public Task<LoginResponseDto> Login(LoginDto userDto);
}
