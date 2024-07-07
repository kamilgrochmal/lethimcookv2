using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.DTO.Documents;
using LetHimCookV2.API.Application.Requests;

namespace LetHimCookV2.API.Application.Services;

public interface IDocumentService
{
    Task UploadDocument(UploadDocument uploadDocument, long patientId);
    Task<DownloadDocumentDto> GetDocument(long documentId, long patientId);
    Task<DocumentInfoDto> GetDocumentInfo(long documentId, long patientId);
    Task<IEnumerable<DocumentDto>> GetDocuments(DateTime? from, DateTime? to, long? catalogId, long patientId);
}