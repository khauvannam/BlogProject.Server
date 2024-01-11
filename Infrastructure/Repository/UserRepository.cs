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
    private readonly IJwtHandler _jwtHandler;

    public UserRepository(
        UserDbContext context,
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtHandler jwtHandler
    )
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtHandler = jwtHandler;
    }

    public async Task Register(RegisterDto model)
    {
        var user = _mapper.Map<RegisterDto, User>(model);
        user.SecurityStamp = Guid.NewGuid().ToString();
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
            new(nameof(ClaimTypes.Name), $"{user.UserName}")
        };
        await _userManager.AddClaimsAsync(user, claims);
    }

    public async Task<LoginResponseDto> Login(LoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(e => e.Email == loginDto.Email);
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
        var claims = await _userManager.GetClaimsAsync(user);
        var accessToken = _jwtHandler.GenerateAccessToken(claims);
        var refreshToken = _jwtHandler.GenerateRefreshToken();
        return new LoginResponseDto() { AccessToken = accessToken, RefreshToken = refreshToken };
    }
}
