using Application.Abstraction;
using Application.Error;
using MediatR;

namespace Application.Tokens.Command;

public class Revoke
{
    public class Command : IRequest<Result<string>>
    {
        public string? Id { get; init; }
    }

    public class Handler(ITokenRepository tokenRepository) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var result = await tokenRepository.Revoke(request.Id!);
            return result;
        }
    }
}
