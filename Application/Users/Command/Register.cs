using MediatR;

namespace Application.Users.Command;

public class Register : IRequest
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
}