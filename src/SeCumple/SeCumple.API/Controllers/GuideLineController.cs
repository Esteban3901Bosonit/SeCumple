using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.GuideLines.Queries.ListGuideLines;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GuideLineController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/configuracion/ListarLineamiento", Name = "GetGuideLineByAxisId")]
    public async Task<IActionResult> GetGuideLineByAxisId(int id)
    {
        var query = new GetGuidelLinesByAxisIdQuery { AxisId = id };
        return Ok(await mediator.Send(query));
    }
}