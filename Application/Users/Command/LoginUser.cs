using Application.Abstraction;
using Application.Error;
using AutoMapper;
using Domain.Entity.Auth;
using Domain.Entity.Users;
using MediatR;

namespace Application.Users.Command;

public class LoginUser
{
    public class Command : LoginDto, IRequest<Result<LoginResponseDto>> { }

    public class Handler : IRequestHandler<Command, Result<LoginResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Handler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<LoginResponseDto>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var user = _mapper.Map<Command, LoginDto>(request);
            var result = await _userRepository.Login(user);
            return result;
        }
    }
}
