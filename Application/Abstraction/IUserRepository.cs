using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;

namespace Application.Abstraction;

public interface IUserRepository
{
    public Task Register(RegisterDTO user);
    public Task<string> Login(LoginDTO userDto);
}
