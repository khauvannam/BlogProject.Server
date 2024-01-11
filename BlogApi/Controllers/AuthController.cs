using Application.Users.Command;
using Application.Users.Queries;
using AutoMapper;
using Domain.Entity.User;
using Domain.Entity.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = _mapper.Map<RegisterDto, RegisterUser.Command>(registerDto);
        await _mediator.Send(user);
        return Ok($"Your account with username {user.UserName} is created successful");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var loginUser = _mapper.Map<LoginDto, LoginUser.Command>(loginDto);
        var loginResponse = await _mediator.Send(loginUser);
        if (!loginResponse.IsLoginSuccessful)
        {
            return Unauthorized(loginResponse.ErrorMessage);
        }

        return Ok(loginResponse);
    }
}
