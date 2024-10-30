using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.GuideLines.Commands.CreateGuideLine;
using SeCumple.Application.Components.GuideLines.Commands.DeleteGuideLine;
using SeCumple.Application.Components.GuideLines.Commands.UpdateGuideline;
using SeCumple.Application.Components.GuideLines.Queries.ListGuideLines;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GuideLineController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/api/lineamiento/ListarLineamiento", Name = "GetGuideLineByAxisId")]
    public async Task<IActionResult> GetGuideLineByAxisId(int id)
    {
        var query = new GetGuidelLinesByAxisIdQuery { AxisId = id };
        return Ok(await mediator.Send(query));
    }

    [HttpPost("~/api/lineamiento/InsertarLineamiento", Name = "CreateGuideLine")]
    public async Task<IActionResult> CreateGuideLine([FromBody] CreateGuidelineCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/lineamiento/ActulizarLineamiento", Name = "UpdateGuideLine")]
    public async Task<IActionResult> UpdateGuideLine([FromBody] UpdateGuidelineCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/lineamiento/EliminarLineamiento", Name = "DeleteGuideLine")]
    public async Task<IActionResult> DeleteGuideLine([FromBody] DeleteGuidelineCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}