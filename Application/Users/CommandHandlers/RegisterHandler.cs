using Application.Abstraction;
using Application.Users.Command;
using Domain.Models;
using MediatR;

namespace Application.Users.CommandHandlers;

public class RegisterHandler : IRequestHandler<Register>
{
    private readonly IUserRepository _users;

    public RegisterHandler(IUserRepository users)
    {
        _users = users;
    }

    public Task Handle(Register request, CancellationToken cancellationToken)
    {
        var user = new RegisterUserDto
        {
            UserName = request.UserName, Password = request.Password, Email = request.Email,
            ConfirmPassword = request.ConfirmPassword
        };
        return _users.Register(user);
    }
}