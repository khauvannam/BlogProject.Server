using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserRepository(UserDbContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<User> Register(RegisterUserDto userModel)
    {
        var user = _mapper.Map<RegisterUserDto, User>(userModel);
        var result = await _userManager.CreateAsync(user, userModel.Password);
        if (result.Succeeded)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await _userManager.AddClaimAsync(user, new Claim("UserIdentity", $@"{user.Id}"));
        }
        else
        {
            foreach (var error in result.Errors)
            {
                throw new Exception($"{error}");
            }
        }
        return user;
    }

    public Task<List<Post>> Login(User user)
    {
        throw new NotImplementedException();
    }
}
