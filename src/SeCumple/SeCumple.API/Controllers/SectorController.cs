using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Sectors.Commands.CreateSector;
using SeCumple.Application.Components.Sectors.Commands.DeleteSector;
using SeCumple.Application.Components.Sectors.Commands.UpdateSector;
using SeCumple.Application.Components.Sectors.Queries.ListSector;
using SeCumple.Application.Components.Sectors.Queries.SelectSector;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SectorController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/api/configuracion/ListarSector", Name = "SelectSector")]
    public async Task<IActionResult> SelectSector()
    {
        var query = new SelectSectorQuery();
        return Ok(await mediator.Send(query));
    }
    
    [HttpPost]
    [Route("~/api/sector/ListarSector", Name = "listSector")]
    public async Task<IActionResult> ListSector(ListSectorsQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/sector/InsertarSector", Name = "createSector")]
    public async Task<IActionResult> CreateSector([FromBody] CreateSectorCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/sector/ActulizarSector", Name = "UpdateSector")]
    public async Task<IActionResult> UpdateSector([FromBody] UpdateSectorCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/sector/EliminarSector", Name = "DeleteSector")]
    public async Task<IActionResult> DeleteSector([FromBody] DeleteSectorCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}