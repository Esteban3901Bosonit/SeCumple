using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Axles.Commands.CreateAxis;
using SeCumple.Application.Components.Axles.Commands.DeleteAxis;
using SeCumple.Application.Components.Axles.Commands.UpdateAxis;
using SeCumple.Application.Components.Axles.Commands.ValidateAxis;
using SeCumple.Application.Components.Axles.Queries.ListAxis;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AxisController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/api/Eje/ListarEjeLineamiento", Name = "getAxisByDocumentId")]
    public async Task<IActionResult> GetAxisByDocumentId(int id)
    {
        var query = new GetAxisByDocumentIdQuery { DocumentId = id };
        return Ok(await mediator.Send(query));
    }
    
    [HttpPost("~/api/Eje/InsertarEje", Name = "CreateAxis")]
    public async Task<IActionResult> CreateAxis([FromBody] CreateAxisCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/Eje/ActulizarEje", Name = "UpdateAxis")]
    public async Task<IActionResult> UpdateDocument([FromBody] UpdateAxisCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/Eje/ValidarEje", Name = "ValidateAxis")]
    public async Task<IActionResult> ValidateDocument([FromBody] ValidateAxisCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPost("~/api/Eje/EliminarEje", Name = "DeleteAxis")]
    public async Task<IActionResult> DeleteDocument([FromBody] DeleteAxisCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}