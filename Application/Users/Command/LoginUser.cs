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

    public class Handler(IUserRepository userRepository, IMapper mapper)
        : IRequestHandler<Command, Result<LoginResponseDto>>
    {
        public async Task<Result<LoginResponseDto>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var user = mapper.Map<Command, LoginDto>(request);
            var result = await userRepository.Login(user);
            return result;
        }
    }
}
