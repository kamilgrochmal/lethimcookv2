using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Documents;
using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Repositories;
using LetHimCookV2.API.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.Repositories;

internal sealed class DocumentRepository : IDocumentRepository
{
    private readonly LetHimCookV2DbContext _v2DbContext;

    public DocumentRepository(LetHimCookV2DbContext v2DbContext)
    {
        _v2DbContext = v2DbContext;
    }

    public async Task UploadDocument(Document document)
    {
        await _v2DbContext.AddAsync(document);
        await _v2DbContext.SaveChangesAsync();
    }

    public async Task<Document> GetDocument(DocumentId documentId, PatientId patientId)
    {
        return await _v2DbContext.Documents.SingleOrDefaultAsync(a => a.DocumentId == documentId)!;
    }

    public async Task<IEnumerable<Document>> GetDocuments(DateTime? from, DateTime? to,
        long? catalogId, PatientId patientId)
    {
        var documents = _v2DbContext.Documents.Where(a => a.PatientId == patientId);

        if (catalogId is not null)
        {
            documents = documents.Where(a => a.CatalogId == new CatalogId(catalogId.Value));
        }

        if (from is not null)
        {
            documents = documents.Where(a => a.DocumentDate >= from.Value);
        }

        if (to is not null)
        {
            documents = documents.Where(a => a.DocumentDate <= to.Value);
        }

        return await documents.AsNoTracking().ToListAsync();
    }
}