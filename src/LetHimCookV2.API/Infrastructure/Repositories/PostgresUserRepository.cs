using LetHimCookV2.API.Domain.Repositories;
using LetHimCookV2.API.Domain.Users;
using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.Repositories;

internal sealed class PostgresUserRepository : IUserRepository
{
    private readonly LetHimCookDbContext _dbContext;
    private readonly DbSet<User> _users;

    public PostgresUserRepository(LetHimCookDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = dbContext.Users;
    }

    public Task<User> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User> GetByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User> GetByUsernameAsync(Username username)
        => _users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}