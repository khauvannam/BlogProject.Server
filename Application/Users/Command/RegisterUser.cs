using Application.Abstraction;
using Application.Error;
using AutoMapper;
using Domain.Entity.User;
using MediatR;

namespace Application.Users.Command;

public class RegisterUser
{
    public class Command : RegisterDto, IRequest<Result<string>> { }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IUserRepository _users;

        private readonly IMapper _mapper;

        public Handler(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var user = _mapper.Map<Command, RegisterDto>(request);
            var result = await _users.Register(user);
            return result;
        }
    }
}
