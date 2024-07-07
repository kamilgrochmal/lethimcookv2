using LetHimCookV2.API.Application.Security;
using LetHimCookV2.API.Domain.Users;
using LetHimCookV2.API.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace LetHimCookV2.API.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddTransient<IIdentityService, IdentityService>();

        return services;
    }
}