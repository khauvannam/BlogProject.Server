using Application.Abstraction;
using AutoMapper;
using Domain.Entity.User;
using MediatR;

namespace Application.Users.Command;

public class RegisterUser
{
    public class Command : RegisterDto, IRequest { }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public Handler(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        public Task Handle(Command request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Command, RegisterDto>(request);
            return _users.Register(user);
        }
    }
}
