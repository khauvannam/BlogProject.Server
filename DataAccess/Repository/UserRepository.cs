using Application.Abstraction;
using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly SocialDbContext _context;

    public UserRepository(SocialDbContext context)
    {
        _context = context;
    }

    public async Task<User> Register(RegisterUserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<List<Post>> Login(User user)
    {
        throw new NotImplementedException();
    }
}