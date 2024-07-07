using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Repositories;
using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly LetHimCookV2DbContext _v2DbContext;

    public CatalogRepository(LetHimCookV2DbContext v2DbContext)
    {
        _v2DbContext = v2DbContext;
    }
    
    public async Task CreateCatalog(Catalog catalog)
    {
        await _v2DbContext.Catalogs.AddAsync(catalog);
        await _v2DbContext.SaveChangesAsync();
    }

    public async Task<Catalog> GetCatalog(CatalogId catalogId)
    {
       return await _v2DbContext.Catalogs.SingleOrDefaultAsync(a => a!.CatalogId == catalogId);
    }

    public async Task UpdateCatalog(Catalog catalog)
    {
        _v2DbContext.Catalogs.Update(catalog);
        await _v2DbContext.SaveChangesAsync();
    }

  
}