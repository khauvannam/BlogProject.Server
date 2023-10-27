using Application.Abstraction;
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

    public async Task<User> Register(User user)
    {
        if (await _context.Users.AnyAsync(u => u.Username == user.Username))
        {
            throw new Exception("UserName is already taken");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<List<Post>> Login(User user)
    {
        throw new NotImplementedException();
    }
}