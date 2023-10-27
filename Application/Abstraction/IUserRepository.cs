using Domain.Models;

namespace Application.Abstraction;

public interface IUserRepository
{
    public Task<User> Register(User user);
    public Task<List<Post>> Login(User user);
}