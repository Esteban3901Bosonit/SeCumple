using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ParameterController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/api/parameter/ListarParameter", Name = "listParameter")]
    public async Task<IActionResult> ListParameter()
    {
        var query = new SelectParameterDetailsQuery();
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet]
    [Route("~/api/parameter/ListarParameterDet", Name = "listParameterDetails")]
    public async Task<IActionResult> ListParameterDetails(int id)
    {
        var query = new SelectParameterDetailsQuery { ParentId = id };
        return Ok(await mediator.Send(query));
    }
}