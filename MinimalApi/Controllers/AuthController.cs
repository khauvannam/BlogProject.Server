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
    public async Task<IActionResult> Register([FromForm] RegisterUserDto registerUserDto)
    {
        return Ok();
    }

    public async Task<IActionResult> Login([FromForm] RegisterUserDto registerUserDto)
    {
        return NoContent();
    }
}
