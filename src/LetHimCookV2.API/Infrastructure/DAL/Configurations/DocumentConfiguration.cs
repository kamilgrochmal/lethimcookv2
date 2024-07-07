using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Documents;
using LetHimCookV2.API.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetHimCookV2.API.Infrastructure.DAL.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(a => a.DocumentId);
        builder.Property(a => a.DocumentId)
            .HasConversion(a => a.Id, a => new DocumentId(a)).ValueGeneratedOnAdd();

        builder.HasOne(d => d.Catalog)
            .WithMany(c => c.Documents)
            .HasForeignKey(d => d.CatalogId)
            .IsRequired();

        builder.HasOne(d => d.Patient)
            .WithMany(p => p.Documents)
            .HasForeignKey(d => d.PatientId)
            .IsRequired();

        builder.Property(d => d.CatalogId)
            .HasConversion(id => id.Id, id => new CatalogId(id));
        
        builder.Property(d => d.PatientId)
            .HasConversion(id => id.Id, id => new PatientId(id));
    }
}