using MediatR;

namespace Application.Users.Command;

public class Register : IRequest
{
    public string Email { get; init; }

    public string UserName { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; set; }
}