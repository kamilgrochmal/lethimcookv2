using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Application.Services;
using LetHimCookV2.API.Domain.Users;
using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly LetHimCookDbContext _dbContext;

    public UserService(LetHimCookDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<UserDto> Get(GetUser query)
    {
        var userId = new UserId(query.Id);
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId);

        return user?.AsDto();
    }
}