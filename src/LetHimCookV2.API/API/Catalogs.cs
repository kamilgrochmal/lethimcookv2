using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace LetHimCookV2.API.API;

public class Catalogs : BaseController
{
    private readonly ICatalogsService _catalogsService;

    public Catalogs(ICatalogsService catalogsService)
    {
        _catalogsService = catalogsService;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Returns available catalogs")]
    public async Task<ActionResult<IEnumerable<CatalogDto>>> Get()
    {
        return Ok(await _catalogsService.BrowseAsync());
    }
}

public interface ICatalogsService
{
    Task<List<CatalogDto>> BrowseAsync();
}

public class CatalogsService : ICatalogsService
{
    private readonly LetHimCookV2DbContext _v2DbContext;

    public CatalogsService(LetHimCookV2DbContext v2DbContext)
    {
        _v2DbContext = v2DbContext;
    }
    public async Task<List<CatalogDto>> BrowseAsync()
    {
        return await _v2DbContext
            .Catalogs
            .Select(a => new CatalogDto(a.CatalogId.Id, a.Title, a.Description))
            .AsNoTracking()
            .ToListAsync();
    }
}

public record CatalogDto(long Id, string Name, string Description);