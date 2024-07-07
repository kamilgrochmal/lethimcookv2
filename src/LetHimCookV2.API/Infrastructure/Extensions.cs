using LetHimCookV2.API.Application.Security;
using LetHimCookV2.API.Infrastructure.Auth;
using LetHimCookV2.API.Infrastructure.DAL;
using LetHimCookV2.API.Infrastructure.Security;
using LetHimCookV2.API.API;
using LetHimCookV2.API.Application.Services;
using LetHimCookV2.API.Domain.Abstractions;
using LetHimCookV2.API.Domain.Repositories;
using LetHimCookV2.API.Infrastructure.Exceptions;
using LetHimCookV2.API.Infrastructure.Repositories;
using LetHimCookV2.API.Infrastructure.Services;
using LetHimCookV2.API.Infrastructure.Time;

namespace LetHimCookV2.API.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();

        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<ICatalogRepository, CatalogRepository>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<ICatalogsService,CatalogsService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddSecurity();
        services.AddControllers();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();

        services
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock>();

        services.AddAuth(configuration);
    }
}