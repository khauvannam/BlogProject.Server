using Application.Error;
using Domain.Entity.Auth;
using Domain.Entity.User;
using Domain.Entity.Users;

namespace Application.Abstraction;

public interface IUserRepository
{
    public Task<Result<string>> Register(RegisterDto user);
    public Task<Result<LoginResponseDto>> Login(LoginDto userDto);

    //TODO ADD RESET PASSWORD, 2 STEP
}
