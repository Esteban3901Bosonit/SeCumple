using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Documents.Queries.ListDocument;

namespace SeCumple.API.Controllers;

public class DocumentController(IMediator mediator) : ControllerBase
{
    [HttpGet("list", Name = "ListDocuments")]
    public async Task<IActionResult> ListDocuments()
    {
        var query = new ListDocumentCommand();
        return Ok(await mediator.Send(query));
    }
}