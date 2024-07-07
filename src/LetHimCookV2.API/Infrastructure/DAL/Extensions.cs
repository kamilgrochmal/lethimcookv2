using LetHimCookV2.API.Common;
using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<LetHimCookDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        services.AddHostedService<DatabaseInitializer>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }


    public static UserDto AsDto(this User entity)
        => new()
        {
            Id = entity.Id,
            Username = entity.Username
        };
}