using Domain.Entity.Post;
using Domain.Entity.User;

namespace Application.Abstraction;

public interface IUserRepository
{
    public Task Register(RegisterDto user);
    public Task<string> Login(LoginDto userDto);
}
