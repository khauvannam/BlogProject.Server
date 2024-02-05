using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;
using Domain.Entity.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ISender mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = mapper.Map<RegisterDto, RegisterUser.Command>(registerDto);
        var result = await mediator.Send(user);
        return result.IsFailure
            ? BadRequest(result.Errors)
            : Ok($"Your account with username {result.Value} is created successful");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var loginUser = mapper.Map<LoginDto, LoginUser.Command>(loginDto);
        var result = await mediator.Send(loginUser);
        if (result.IsFailure)
        {
            return Unauthorized(result.Errors);
        }
        return Ok(result.Value);
    }
}
