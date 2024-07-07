using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LetHimCookDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (!await dbContext.Catalogs.AnyAsync(cancellationToken: cancellationToken))
        {
            await dbContext.Catalogs.AddRangeAsync(
                new Catalog("Medical Records", "Medical history of patients"),
                new Catalog("Insurance Documents", "Insurance related documents")
            );

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}