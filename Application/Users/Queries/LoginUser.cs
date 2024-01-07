using Application.Abstraction;
using AutoMapper;
using Domain.Entity.Auth;
using Domain.Entity.User;
using Domain.Entity.Users;
using MediatR;

namespace Application.Users.Queries;

public class LoginUser
{
    public class Command : LoginDto, IRequest<LoginResponseDto> { }

    public class Handler : IRequestHandler<Command, LoginResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Handler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<LoginResponseDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Command, LoginDto>(request);
            return _userRepository.Login(user);
        }
    }
}
