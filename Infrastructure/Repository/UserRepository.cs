﻿using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Post;
using Domain.Entity.User;
using Microsoft.AspNetCore.Identity;

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

    public async Task Register(RegisterDto model)
    {
        var user = _mapper.Map<RegisterDto, User>(model);
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new Exception(
                $"Your username {user.UserName} or email {user.Email} have been used"
            );
        }
        var claims = new List<Claim>
        {
            new Claim("UserIdentity", $"{user.Id}"),
            new Claim("RoleIdentity", $"{model.SetRole.ToString()}")
        };
        await _userManager.AddClaimsAsync(user, claims);
    }

    public Task<string> Login(LoginDto userDto)
    {
        throw new NotImplementedException();
    }
}