using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Sectors.Queries.ListSector;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SectorController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/configuracion/ListarSector", Name = "SelectSector")]
    public async Task<IActionResult> SelectSector()
    {
        var query = new ListSectorQuery();
        return Ok(await mediator.Send(query));
    }
}