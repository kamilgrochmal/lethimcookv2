using LetHimCookV2.API.Domain.Catalogs;

namespace LetHimCookV2.API.Domain.Repositories;

public interface ICatalogRepository
{
    Task CreateCatalog(Catalog catalog);
    Task<Catalog> GetCatalog(CatalogId catalogId);
    Task UpdateCatalog(Catalog catalog);
}