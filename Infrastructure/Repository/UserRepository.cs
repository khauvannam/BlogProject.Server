using System.Security.Claims;
using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Auth;
using Domain.Entity.User;
using Domain.Entity.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtHandler _jwtHandler;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserRepository(
        UserDbContext context,
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtHandler jwtHandler,
        IHttpContextAccessor contextAccessor
    )
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtHandler = jwtHandler;
        _contextAccessor = contextAccessor;
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
        if (user is null)
            throw new Exception("The user is not exists");
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
        await SaveTokenToDb(user.Id, refreshToken);

        return new LoginResponseDto() { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    private async Task SaveTokenToDb(string userId, string? token)
    {
        var existingToken = _context.Tokens.FirstOrDefault(t => t.UserId == userId);

        if (existingToken is not null)
        {
            existingToken.RefreshToken = token;
            existingToken.ExpiredIn = DateTime.Now.AddMonths(1);
        }
        else
        {
            var refreshToken = new Token()
            {
                UserId = userId,
                RefreshToken = token,
                ExpiredIn = DateTime.Now.AddMonths(1),
            };

            _context.Tokens.Add(refreshToken);
        }

        await _context.SaveChangesAsync();
    }
}
