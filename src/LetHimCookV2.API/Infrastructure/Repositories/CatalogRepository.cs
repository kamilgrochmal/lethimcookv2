using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Repositories;
using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly LetHimCookDbContext _dbContext;

    public CatalogRepository(LetHimCookDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateCatalog(Catalog catalog)
    {
        await _dbContext.Catalogs.AddAsync(catalog);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Catalog> GetCatalog(CatalogId catalogId)
    {
       return await _dbContext.Catalogs.SingleOrDefaultAsync(a => a!.CatalogId == catalogId);
    }

    public async Task UpdateCatalog(Catalog catalog)
    {
        _dbContext.Catalogs.Update(catalog);
        await _dbContext.SaveChangesAsync();
    }

  
}