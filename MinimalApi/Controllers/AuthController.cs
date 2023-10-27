using Application.Users.Command;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("register")]
    public async Task<IActionResult> Register([FromForm] UserDTO userDto)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        var user = new Register { UserName = userDto.UserName, PasswordHash = passwordHash };
        await _mediator.Send(user);
        return Ok(user);
    }

    public async Task<IActionResult> Login()
    {
        
    }
}