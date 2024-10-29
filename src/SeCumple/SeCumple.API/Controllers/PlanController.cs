using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;
using SeCumple.Application.Components.Plans.Commands.CreatePlan;
using SeCumple.Application.Components.Plans.Queries.ListPlan;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PlanController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/api/gestion/listarPlan", Name = "listPlan")]
    public async Task<IActionResult> ListPlans(ListPlanQuery request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPost]
    [Route("~/api/gestion/InsertarPlanCumplimiento", Name = "CreatePlan")]
    public async Task<IActionResult> CreatePlan(CreatePlanCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpGet("select", Name = "SelectPlans")]
    public async Task<IActionResult> SelectPlans()
    {
        var query = new SelectParameterDetailsQuery();
        return Ok(await mediator.Send(query));
    }
    
    [HttpPost]
    [Route("~/api/gestion/ListarPlanCumplimientoAnio", Name = "ListPlanAnio")]
    public async Task<IActionResult> ListPlanAnio(int id)
    {
        var query = new SelectParameterDetailsQuery();
        return Ok(await mediator.Send(query));
    }
}