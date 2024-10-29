using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Axles.Queries.ListAxis;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AxisController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/configuracion/listarEje", Name = "getAxisByDocumentId")]
    public async Task<IActionResult> GetAxisByDocumentId(int id)
    {
        var query = new GetAxisByDocumentIdQuery { DocumentId = id };
        return Ok(await mediator.Send(query));
    }
}