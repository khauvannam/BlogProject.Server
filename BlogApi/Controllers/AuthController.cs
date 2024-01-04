using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;
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

    [HttpGet("login")]
    public IActionResult Login()
    {
        // Your login logic
        return Ok("Login Successful");
    }
}
