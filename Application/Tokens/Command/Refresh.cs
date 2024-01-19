using Application.Abstraction;
using Domain.Entity.Auth;
using MediatR;

namespace Application.Tokens.Command;

public class Refresh
{
    public class Command : IRequest<TokenDto>
    {
        public string AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class Handler : IRequestHandler<Command, TokenDto>
    {
        private readonly ITokenRepository _tokenRepository;

        public Handler(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public Task<TokenDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var token = new TokenDto(request.AccessToken, request.RefreshToken);
            return _tokenRepository.Refresh(token);
        }
    }
}
