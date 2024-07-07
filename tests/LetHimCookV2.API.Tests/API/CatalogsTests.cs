using System.Net.Http.Json;
using FluentAssertions;
using LetHimCookV2.API.API;

namespace LetHimCookV2.API.Tests.API;

public class CatalogsTests : ControllerTests, IDisposable
{
    private readonly TestDatabase _testDatabase;
    public CatalogsTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    [Fact]
    public async Task GET_Catalogs_ReturnsAvailableCatalogs()
    {
        Authorize(1, "user");
        var response = await Client.GetAsync("Catalogs");
        var catalogs = await response.Content.ReadFromJsonAsync<IEnumerable<CatalogDto>>();
        catalogs.Should().NotBeNull();
    }
}