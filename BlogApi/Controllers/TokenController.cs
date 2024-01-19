using System.Security.Claims;
using Application.Tokens.Command;
using Domain.Entity.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(TokenDto tokenDto)
    {
        var token = new Refresh.Command
        {
            RefreshToken = tokenDto.RefreshToken,
            AccessToken = tokenDto.AccessToken
        };
        var refreshToken = await _mediator.Send(token);

        return Ok(refreshToken);
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke()
    {
        var userId = User.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
        if (userId is null)
        {
            return BadRequest();
        }
        var revokeToken = new Revoke.Command { Id = userId };
        await _mediator.Send(revokeToken);
        return NoContent();
    }
}
