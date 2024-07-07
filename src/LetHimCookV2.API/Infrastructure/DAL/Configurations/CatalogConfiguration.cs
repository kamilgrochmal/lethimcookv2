using LetHimCookV2.API.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetHimCookV2.API.Infrastructure.DAL.Configurations;

internal sealed class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(a => a.CatalogId);
        builder.Property(a => a.CatalogId)
            .HasConversion(a => a.Id, a => new CatalogId(a)).ValueGeneratedOnAdd();
    }
}