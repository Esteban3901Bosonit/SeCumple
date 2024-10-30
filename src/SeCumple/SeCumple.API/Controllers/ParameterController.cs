using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.ParameterDetails.Commands.CreateParameterDetail;
using SeCumple.Application.Components.ParameterDetails.Commands.DeleteParameterDetail;
using SeCumple.Application.Components.ParameterDetails.Commands.UpdateParameterDetail;
using SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;
using SeCumple.Application.Components.Parameters.Queries.ListParameters;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ParameterController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/api/parameter/ListarParameter", Name = "listParameter")]
    public async Task<IActionResult> ListParameter(ListParametersQuery request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpGet]
    [Route("~/api/parameter/ListarParameterDet", Name = "listParameterDetails")]
    public async Task<IActionResult> ListParameterDetails(int id)
    {
        var query = new SelectParameterDetailsQuery { ParentId = id };
        return Ok(await mediator.Send(query));
    }

    [HttpPost("~/api/parameter/InsertarParameterDet", Name = "CreateParameterDetail")]
    public async Task<IActionResult> CreateParameterDetail([FromBody] CreateParameterDetailCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/parameter/ActulizarParameter", Name = "UpdateParameterDetail")]
    public async Task<IActionResult> UpdateParameterDetail([FromBody] UpdateParameterDetailCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/parameter/EliminarParameterDet", Name = "DeleteParameterDetail")]
    public async Task<IActionResult> DeleteParameterDetail([FromBody] DeleteParameterDetailCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}