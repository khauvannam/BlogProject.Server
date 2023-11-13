using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Controllers;

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

    [HttpGet("register")]
    public async Task<IActionResult> Register([FromForm] RegisterUserDto registerUserDto)
    {
        var user = _mapper.Map<RegisterUserDto, Register>(registerUserDto);
        var newUser = await _mediator.Send(user);
        return Ok(newUser);
    }

    public async Task<IActionResult> Login([FromForm] RegisterUserDto registerUserDto)
    {
        return NoContent();
    }
}
