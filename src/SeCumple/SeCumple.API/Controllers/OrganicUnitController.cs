using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.OrganicUnits.Commands.CreateOrganicUnit;
using SeCumple.Application.Components.OrganicUnits.Commands.DeleteOrganicUnit;
using SeCumple.Application.Components.OrganicUnits.Commands.UpdateOrganicUnit;
using SeCumple.Application.Components.OrganicUnits.Queries.ListOrganicUnit;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrganicUnitController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/api/UnidadOrganica/ListarUnidadOrganica", Name = "ListOrganicUnitBySectorId")]
    public async Task<IActionResult> ListOrganicUnitBySectorId(int iMaeSector)
    {
        var query = new ListOrganicUnitBySectorIdQuery { SectorId = iMaeSector };
        return Ok(await mediator.Send(query));
    }

    [HttpPost("~/api/UnidadOrganica/InsertarUnidadOrganica", Name = "createOrganicUnit")]
    public async Task<IActionResult> CreateOrganicUnit([FromBody] CreateOrganicUnitCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/UnidadOrganica/ActulizarUnidadOrganica", Name = "UpdateOrganicUnit")]
    public async Task<IActionResult> UpdateOrganicUnit([FromBody] UpdateOrganicUnitCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/UnidadOrganica/EliminarUnidadOrganica", Name = "DeleteOrganicUnit")]
    public async Task<IActionResult> DeleteOrganicUnit([FromBody] DeleteOrganicUnitCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}