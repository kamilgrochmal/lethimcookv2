using LetHimCookV2.API.Application.DTO.Documents;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LetHimCookV2.API.API;


public class Documents : BaseController
{
    private readonly IDocumentService _documentService;

    public Documents(IDocumentService documentService)
    {
        _documentService = documentService;
    }


    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Uploads a document")]
    public async Task<IActionResult> UploadDocument([FromForm] UploadDocument uploadDocument)
    {
        if (uploadDocument.File.Length == 0)
            return BadRequest("No file uploaded.");

        var userId = Int64.Parse(User.Identity.Name);
        await _documentService.UploadDocument(uploadDocument, userId);

        return Ok("File uploaded successfully.");
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Returns document for download")]
    public async Task<ActionResult> GetDocument(long id)
    {
        var userId = Int64.Parse(User.Identity.Name);
        var document = await _documentService.GetDocument(id, userId);

        if (document is null)
            return NotFound();

        return File(document.FileContent, document.FileType, document.FileName);
    }

    [Authorize]
    [HttpGet("{id}/details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Returns document info")]
    public async Task<ActionResult<DocumentInfoDto>> GetDocumentInfo(long id)
    {
        var userId = Int64.Parse(User.Identity.Name);
        var documentInfo = await _documentService.GetDocumentInfo(id, userId);

        if (documentInfo is null)
            return NotFound();

        var downloadLink = Url.Action("GetDocument", "Documents", new { id }, Request.Scheme);
        var response = documentInfo with { DownloadLink = downloadLink };
        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Returns a list of documents within a date range or by catalog ID.")]
    public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments([FromQuery] DateTime? from, [FromQuery] DateTime? to,
        [FromQuery] long? catalogId)
    {
        var userId = Int64.Parse(User.Identity.Name);
        var documents = await _documentService.GetDocuments(from, to, catalogId, userId);

        return Ok(documents);
    }
}