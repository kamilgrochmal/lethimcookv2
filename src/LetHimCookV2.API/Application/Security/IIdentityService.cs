using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Domain.Abstractions;

namespace LetHimCookV2.API.Application.Security;

public interface IIdentityService
{
    Task<UserDto> GetAsync(long id);
    Task<JsonWebToken> SignInAsync(SignIn dto);
    Task SignUpAsync(SignUp dto);
}