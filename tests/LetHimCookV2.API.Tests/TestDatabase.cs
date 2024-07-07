using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Tests;

internal sealed class TestDatabase : IDisposable
{
    public LetHimCookV2DbContext Context { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("postgres");
        Context = new LetHimCookV2DbContext(new DbContextOptionsBuilder<LetHimCookV2DbContext>().UseNpgsql(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}