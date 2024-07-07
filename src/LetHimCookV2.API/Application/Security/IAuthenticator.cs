using LetHimCookV2.API.Application.DTO;

namespace LetHimCookV2.API.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(long userId, string role);
}