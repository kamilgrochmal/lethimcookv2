using LetHimCookV2.API.Application.Requests;

namespace LetHimCookV2.API.Application.Services;

public interface IAuthenticationService
{
    Task SignUp(SignUp signUp);
    Task SignIn(SignIn signIn);
}