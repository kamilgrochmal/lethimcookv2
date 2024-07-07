using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace LetHimCookV2.API.Tests;

public class LetHimCookV2TestApp : WebApplicationFactory<IApiMarker>
{
    public HttpClient Client { get; }

    public LetHimCookV2TestApp(Action<IServiceCollection> services = null)
    {
        Client = WithWebHostBuilder(builder =>
        {
            if (services is not null)
            {
                builder.ConfigureServices(services);
            }
            
            builder.UseEnvironment("test");
        }).CreateClient();
    }
}