using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Plans.Commands.ApprovePlan;
using SeCumple.Application.Components.Plans.Commands.CreatePlan;
using SeCumple.Application.Components.Plans.Commands.CreatePlanAnio;
using SeCumple.Application.Components.Plans.Commands.DeletePlan;
using SeCumple.Application.Components.Plans.Commands.DeletePlanAnio;
using SeCumple.Application.Components.Plans.Commands.Duplicate;
using SeCumple.Application.Components.Plans.Commands.UpdatePlan;
using SeCumple.Application.Components.Plans.Queries.GetPlanAnioByPlanId;
using SeCumple.Application.Components.Plans.Queries.ListPlan;
using SeCumple.Application.Components.Plans.Queries.SelectPlan;

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

    [HttpPost]
    [Route("~/api/gestion/EditarPlanCumplimiento", Name = "updatePlan")]
    public async Task<IActionResult> UpdatePlan(UpdatePlanCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/EliminarPlanCumplimiento", Name = "DeletePlan")]
    public async Task<IActionResult> DeletePlan(DeletePlanCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpGet("~/api/configuracion/ListarPlanCumplimiento", Name = "SelectPlans")]
    public async Task<IActionResult> SelectPlans()
    {
        var query = new SelectPlanQuery();
        return Ok(await mediator.Send(query));
    }

    [HttpGet]
    [Route("~/api/gestion/ListarPlanCumplimientoAnio", Name = "ListPlanAnio")]
    public async Task<IActionResult> ListPlanAnio(int id)
    {
        var query = new GetPlanAnioByPlanIdQuery { PlanId = id };
        return Ok(await mediator.Send(query));
    }

    [HttpPost]
    [Route("~/api/gestion/InsertarPlanCumplimientoAnio", Name = "CreatePlanAnio")]
    public async Task<IActionResult> CreatePlanAnio(CreatePlanAnioCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/EliminarPlanCumplimientoAnio", Name = "DeletePlanAnio")]
    public async Task<IActionResult> DeletePlanAnio(DeletePlanAnioCommand request)
    {
        return Ok(await mediator.Send(request));
    }  
    
    [HttpPost]
    [Route("~/api/gestion/EditarEstadoPlanCumplimiento", Name = "UpdataPlanStatus")]
    public async Task<IActionResult> UpdataPlanStatus([FromForm]UpdatePlanStatusCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPost]
    [Route("~/api/gestion/DuplicarPlanCumplimiento", Name = "DuplicatePlan")]
    public async Task<IActionResult> DuplicatePlan(DuplicatePlanCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}