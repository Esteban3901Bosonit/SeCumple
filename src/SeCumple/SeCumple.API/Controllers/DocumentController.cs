using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Documents.Commands.CreateDocument;
using SeCumple.Application.Components.Documents.Queries.GetDocumentById;
using SeCumple.Application.Components.Documents.Queries.ListDocument;
using SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;
using SeCumple.CrossCutting.Enums;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DocumentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/configuracion/ListarDispositivo", Name = "ListDocuments")]    
    public async Task<IActionResult> ListDocuments(ListDocumentQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpGet]
    [Route("~/configuracion/ListarTipoDispositivo", Name = "SelectDocumentTypes")]
    public async Task<IActionResult> SelectDocumentTypes()
    {
        var query = new SelectParameterDetailsQuery { ParentId = (int)ParameterEnum.DocumentType };
        return Ok(await mediator.Send(query));
    }

    [HttpGet("get", Name = "ListarDispositivoPorTipo")]
    public async Task<IActionResult> GetDocumentsByDocumentTypeId(int id)
    {
        var query = new GetDocumentByDpcumentTypeIdQuery { DocumentTypeId = id };
        return Ok(await mediator.Send(query));
    }

    [HttpPost("create", Name = "CreateDocument")]
    public async Task<IActionResult> CreateDocuments([FromBody] CreateDocumentCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("update", Name = "UpdateDocument")]
    public async Task<IActionResult> UpdateDocument([FromBody] CreateDocumentCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}