using Application.Abstraction;
using Application.Users.Command;
using AutoMapper;
using Domain.Entity.User;
using Domain.Models;
using MediatR;

namespace Application.Users.CommandHandlers;

public class RegisterHandler : IRequestHandler<Register>
{
    private readonly IUserRepository _users;
    private readonly IMapper _mapper;

    public RegisterHandler(IUserRepository users, IMapper mapper)
    {
        _users = users;
        _mapper = mapper;
    }

    public Task Handle(Register request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Register, RegisterDTO>(request);
        return _users.Register(user);
    }
}
