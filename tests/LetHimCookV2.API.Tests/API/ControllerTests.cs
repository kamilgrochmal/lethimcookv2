using System.Net.Http.Headers;
using LetHimCookV2.API.Domain.Abstractions;
using LetHimCookV2.API.Infrastructure.Auth;
using LetHimCookV2.API.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace LetHimCookV2.API.Tests.API;

[Collection("api")]
public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    private readonly IAuthManager _authManager;
    protected HttpClient Client { get; }

    protected JsonWebToken Authorize(long userId, string role)
    {
        var jwt = _authManager.CreateToken(userId.ToString(), role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }

    public ControllerTests(OptionsProvider optionsProvider)
    {
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authManager = new AuthManager(authOptions, new Clock());
        var app = new LetHimCookV2TestApp(ConfigureServices);
        Client = app.Client;
    }
    
    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }
}