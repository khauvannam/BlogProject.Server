using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Post;
using Domain.Entity.User;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(
        UserDbContext context,
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager
    )
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task Register(RegisterUserDto userModel)
    {
        var user = _mapper.Map<RegisterUserDto, User>(userModel);
        var result = await _userManager.CreateAsync(user, userModel.Password);
        if (result.Succeeded)
        {
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
    }

    public Task<List<Post>> Login(User user)
    {
        throw new NotImplementedException();
    }
}
