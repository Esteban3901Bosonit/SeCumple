using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Documents.Commands.CreateDocument;
using SeCumple.Application.Components.Documents.Queries.ListDocument;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DocumentController(IMediator mediator) : ControllerBase
{
    [HttpPost("list", Name = "ListDocuments")]
    public async Task<IActionResult> ListDocuments(ListDocumentQuery request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPost("create", Name = "CreateDocument")]
    public async Task<IActionResult> ListDocuments([FromBody]CreateDocumentCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}