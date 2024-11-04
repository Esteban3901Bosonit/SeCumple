using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Files.Commands;
using SeCumple.Application.Components.Files.Queries;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FileController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("downloadFile", Name = "DownloadFile")]
    public async Task<IActionResult> DownloadFile(int id)
    {
        var query = new DownloadFileQuery() { FileId = id };
        var file = await mediator.Send(query);
        return File(file.ContentFile, "application/octet-stream", file.Filename);
    }
    
    
    [HttpPost]
    [Route("uploadFile", Name = "UploadFile")]
    public async Task<IActionResult> UploadFile(UploadFileCommand file)
    {
        var result = await mediator.Send(file);
        return Ok(result);
    }
}