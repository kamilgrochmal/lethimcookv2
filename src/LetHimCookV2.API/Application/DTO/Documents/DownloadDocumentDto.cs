using LetHimCookV2.API.Domain.Documents;

namespace LetHimCookV2.API.Application.DTO.Documents;

public record DownloadDocumentDto(string FileName, byte[] FileContent, string FileType)
{
    public static DownloadDocumentDto ToDto(Document document) =>
        new(document.FileName, document.FileContent, document.FileType);
}

public record DocumentInfoDto(string Title, string Description, DateTime DocumentDate)
{
    public string DownloadLink { get; init; }
    public static DocumentInfoDto ToDto(Document document)
    {
        return new(document.Title, document.Description, document.DocumentDate);
    }
        
}

public record DocumentDto(long Id, string Title, string Description, string DocumentDate)
{
    public static DocumentDto ToDto(Document document)
    {
        return new(document.DocumentId.Id, document.Title, document.Description, document.DocumentDate.ToString("yyyy-MM-dd"));
    }
}