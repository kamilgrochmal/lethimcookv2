namespace LetHimCookV2.API.Application.Requests;

public class UploadDocument
{
    public IFormFile File { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CatalogId { get; set; }
    public DateTime DocumentDate { get; set; }
}