using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Auth;
using Domain.Entity.User;
using Domain.Entity.Users;
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
        user.SecurityStamp = Guid.NewGuid().ToString();
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new Exception(
                $"Your username {user.UserName} or email {user.Email} have been used"
            );
        }
        var claims = new List<Claim>
        {
            new(nameof(ClaimTypes.NameIdentifier), $"{user.Id}"),
            new(nameof(ClaimTypes.Role), $"{model.SetRole.ToString()}"),
        };
        await _userManager.AddClaimsAsync(user, claims);
    }

    public async Task<LoginResponseDto> Login(LoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(e => e.Email == loginDto.Email);
        if (user is null)
            throw new Exception("The user is not valid");
        var result = await _signInManager.PasswordSignInAsync(
            user.UserName,
            loginDto.Password,
            true,
            false
        );
        if (!result.Succeeded)
        {
            return new LoginResponseDto()
            {
                IsLoginSuccessful = false,
                ErrorMessage = "Invalid Authentication"
            };
        }

        return new LoginResponseDto();
    }
}
