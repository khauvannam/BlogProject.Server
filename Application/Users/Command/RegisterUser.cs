using Application.Abstraction;
using Application.Error;
using AutoMapper;
using Domain.Entity.User;
using MediatR;

namespace Application.Users.Command;

public class RegisterUser
{
    public class Command : RegisterDto, IRequest<Result<string>> { }

    public class Handler(IUserRepository users, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var user = mapper.Map<Command, RegisterDto>(request);
            var result = await users.Register(user);
            return result;
        }
    }
}
