using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Requests;

namespace LetHimCookV2.API.Application.Services;

public interface IUserService
{
    Task<UserDto> Get(GetUser query);
}