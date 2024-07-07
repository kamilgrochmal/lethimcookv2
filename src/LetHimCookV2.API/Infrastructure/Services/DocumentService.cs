using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.DTO.Documents;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Application.Services;
using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Documents;
using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Repositories;

namespace LetHimCookV2.API.Infrastructure.Services;

internal sealed class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task UploadDocument(UploadDocument uploadDocument, long patientId)
    {
        //todo chck for catalog existence
        using (var memoryStream = new MemoryStream())
        {
            var file = uploadDocument.File;
            var catalogId = new CatalogId(uploadDocument.CatalogId);
            await file.CopyToAsync(memoryStream);
            var document = new Document
            {
                Title = uploadDocument.Title,
                Description = uploadDocument.Description,
                CreatedDate = DateTime.UtcNow,
                FileContent = memoryStream.ToArray(),
                FileName = file.FileName,
                FileSize = file.Length,
                DocumentDate = uploadDocument.DocumentDate,
                CatalogId = catalogId,
                PatientId = new PatientId(patientId)
            };
            await _documentRepository.UploadDocument(document);
        }
    }

    public async Task<DownloadDocumentDto> GetDocument(long documentId, long patientId)
    {
        var document = await _documentRepository.GetDocument(new DocumentId(documentId), new PatientId(patientId));

        if (document is null)
            return null;

        return DownloadDocumentDto.ToDto(document);
    }

    public async Task<DocumentInfoDto> GetDocumentInfo(long documentId, long patientId)
    {
        var document = await _documentRepository.GetDocument(new DocumentId(documentId), new PatientId(patientId));
        if (document is null)
            return null;

        return DocumentInfoDto.ToDto(document);
    }

    public async Task<IEnumerable<DocumentDto>> GetDocuments(DateTime? from, DateTime? to, long? catalogId,
        long patientId)
    {
        var documents = await _documentRepository.GetDocuments(from, to, catalogId, new PatientId(patientId));

        return documents.Select(DocumentDto.ToDto);
    }
}