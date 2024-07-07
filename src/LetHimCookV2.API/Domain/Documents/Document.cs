using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Patients;

namespace LetHimCookV2.API.Domain.Documents;

public class Document
{
    private const string DocumentType = "application/pdf";
    
    public DocumentId DocumentId { get; set; }
    
    public CatalogId CatalogId { get; set; }
    public Catalog Catalog { get; set; }

    public PatientId PatientId { get; set; }
    public Patient Patient { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public byte[] FileContent { get; set; }
    public string FileName { get; set; }
    public long FileSize { get; set; }
    public string FileType { get; set; } = DocumentType;
    public DateTime DocumentDate { get; set; }
}

public record DocumentId(long Id);