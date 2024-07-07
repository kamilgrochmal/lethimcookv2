using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Documents;
using LetHimCookV2.API.Domain.Patients;

namespace LetHimCookV2.API.Domain.Repositories;

public interface IDocumentRepository
{
    Task UploadDocument(Document document);
    Task<Document> GetDocument(DocumentId documentId, PatientId patientId);
    Task<IEnumerable<Document>> GetDocuments(DateTime? from, DateTime? to, long? catalogId, PatientId patientId);
}