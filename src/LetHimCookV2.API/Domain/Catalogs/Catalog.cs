using LetHimCookV2.API.Domain.Documents;

namespace LetHimCookV2.API.Domain.Catalogs;

public class Catalog
{
    public CatalogId CatalogId { get; init; }
    public string Title { get; private set; }
    public string? Description { get; private set; }

    public ICollection<Document> Documents { get; private set; }

    public Catalog( string title, string? description)
    {
        Title = title;
        Description = description;
    }
}

public record CatalogId(long Id);