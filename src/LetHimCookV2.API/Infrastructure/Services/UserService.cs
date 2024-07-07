using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Application.Services;
using LetHimCookV2.API.Domain.Users;
using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly LetHimCookV2DbContext _v2DbContext;

    public UserService(LetHimCookV2DbContext v2DbContext)
    {
        _v2DbContext = v2DbContext;
    }
    public async Task<UserDto> Get(GetUser query)
    {
        var userId = new UserId(query.Id);
        var user = await _v2DbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}