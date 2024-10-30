using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Documents.Commands.CreateDocument;
using SeCumple.Application.Components.Documents.Commands.DeleteDocument;
using SeCumple.Application.Components.Documents.Commands.UpdateDocument;
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
    [Route("~/api/configuracion/ListarDispositivo", Name = "ListDocuments")]    
    public async Task<IActionResult> ListDocumentsConf(ListDocumentQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpGet]
    [Route("~/api/configuracion/ListarTipoDispositivo", Name = "SelectDocumentTypes")]
    public async Task<IActionResult> SelectDocumentTypes()
    {
        var query = new SelectParameterDetailsQuery { ParentId = (int)ParameterEnum.DocumentType };
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet]
    [Route("~/api/configuracion/ListarDispositivo", Name = "listDocuments")]
    public async Task<IActionResult> ListDocuments(int id)
    {
        var query = new GetDocumentByDpcumentTypeIdQuery { DocumentTypeId = id };
        return Ok(await mediator.Send(query));
    }

    [HttpGet]
    [Route("~/api/Dispositivo/ListarDispositivo", Name = "ListarDispositivoPorTipo")]
    public async Task<IActionResult> GetDocumentsByDocumentTypeId(int id)
    {
        var query = new GetDocumentByDpcumentTypeIdQuery { DocumentTypeId = id };
        return Ok(await mediator.Send(query));
    }

    [HttpPost("~/api/dispositivo/InsertarDispositivo", Name = "CreateDocument")]
    public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/dispositivo/ActulizarDispositivo", Name = "UpdateDocument")]
    public async Task<IActionResult> UpdateDocument([FromBody] UpdateDocumentCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPost("~/api/dispositivo/EliminarDispositivo", Name = "DeleteDocument")]
    public async Task<IActionResult> DeleteDocument([FromBody] DeleteDocumentCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}