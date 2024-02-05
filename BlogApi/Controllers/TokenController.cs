using System.Security.Claims;
using Application.Tokens.Command;
using Domain.Entity.Auth;
using Domain.Entity.ErrorsHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController(ISender mediator) : ControllerBase
{
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(TokenDto tokenDto)
    {
        var tokenCommand = new Refresh.Command
        {
            RefreshToken = tokenDto.RefreshToken,
            AccessToken = tokenDto.AccessToken
        };
        var result = await mediator.Send(tokenCommand);

        return result.IsFailure ? BadRequest(result.Errors) : Ok(result.Value);
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke()
    {
        var userId = User.FindFirstValue(nameof(ClaimTypes.NameIdentifier));
        if (userId is null)
        {
            return BadRequest(UserErrors.NotFound);
        }
        var revokeToken = new Revoke.Command { Id = userId };
        var result = await mediator.Send(revokeToken);
        return result.IsFailure ? BadRequest(result.Errors) : Ok(result.Value);
    }
}
