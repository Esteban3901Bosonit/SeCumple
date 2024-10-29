using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Axles.Queries.ListAxis;
using SeCumple.Application.Components.OrganicUnits.Queries.ListOrganicUnit;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrganicUnitController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("/configuracion/ListarUnidadOrganica", Name = "GetOrganicUnitBySectorId")]
    public async Task<IActionResult> GetOrganicUnitBySectorId(int id)
    {
        var query = new GetOrganicUnitBySectorIdQuery{ SectorId = id };
        return Ok(await mediator.Send(query));
    }
}