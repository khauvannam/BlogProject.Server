using Application.Abstraction;
using Application.Error;
using Domain.Entity.Auth;
using Domain.Entity.ErrorsHandler;
using MediatR;

namespace Application.Tokens.Command;

public class Refresh
{
    public class Command : IRequest<Result<TokenDto>>
    {
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
    }

    public class Handler(
        ITokenRepository tokenRepository,
        IUserServiceRepository userServiceRepository)
        : IRequestHandler<Command, Result<TokenDto>>
    {
        public async Task<Result<TokenDto>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            var accessToken = request.AccessToken;
            var refreshToken = request.RefreshToken;
            if (accessToken is null || refreshToken is null)
            {
                return TokenErrors.Invalid;
            }
            var principal = userServiceRepository.GetClaims(accessToken!);
            var userResult = userServiceRepository.CheckExistingUser(principal);
            if (userResult.IsFailure)
            {
                return userResult.Errors;
            }

            var userId = userResult.Value;
            var tokenResult = userServiceRepository.CheckValidToken(userId, refreshToken);
            if (tokenResult.IsFailure)
            {
                return tokenResult.Errors;
            }

            var result = await tokenRepository.Refresh(principal, tokenResult.Value!);
            return result;
        }
    }
}
