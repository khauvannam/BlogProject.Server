using Application.Abstraction;
using MediatR;

namespace Application.Tokens.Command;

public class Revoke
{
    public class Command : IRequest
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly ITokenRepository _tokenRepository;

        public Handler(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            await _tokenRepository.Revoke(request.Id);
        }
    }
}
