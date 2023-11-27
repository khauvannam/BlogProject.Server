using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;

namespace Application.Abstraction;

public interface IUserRepository
{
    public Task Register(RegisterUserDto user);
    public Task<List<Post>> Login(User user);
}
