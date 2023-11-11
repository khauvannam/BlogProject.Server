using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(UserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
