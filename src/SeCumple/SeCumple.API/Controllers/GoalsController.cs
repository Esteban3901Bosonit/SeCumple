using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Goals.Commands.CreateGoal;
using SeCumple.Application.Components.Goals.Queries.ListGoals;
using SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;
using SeCumple.CrossCutting.Enums;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GoalsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/api/gestion/ListarMetas", Name = "ListGoals")]
    public async Task<IActionResult> ListGoals(int _iMovIndicadorPeriodo)
    {
        ListGoalsQuery request = new ListGoalsQuery
        {
            iMovIndicadorPeriodo = _iMovIndicadorPeriodo
        };
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/InsertarMetas", Name = "CreateGoal")]
    public async Task<IActionResult> CreateIndicator(CreateGoalCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpGet]
    [Route("~/api/configuracion/ListarUnidadMedida", Name = "listDocuments")]
    public async Task<IActionResult> ListDocuments(int id)
    {
        var query = new SelectParameterDetailsQuery() { ParentId = (int)ParameterEnum.MeasurementUnit };
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet]
    [Route("~/api/configuracion/ListarPeriodicidad", Name = "ListPeriodicity")]
    public async Task<IActionResult> ListPeriodicity(int id)
    {
        var query = new SelectParameterDetailsQuery() { ParentId = (int)ParameterEnum.Periodicity };
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet]
    [Route("~/api/configuracion/ListarEstadoMetaCumplida", Name = "ListarEstadoMetaCumplida")]
    public async Task<IActionResult> ListarEstadoMetaCumplida(int id)
    {
        var query = new SelectParameterDetailsQuery() { ParentId = (int)ParameterEnum.GoalComplianceStatus };
        return Ok(await mediator.Send(query));
    }
    
    [HttpGet]
    [Route("~/api/configuracion/ListarTipoMedicion", Name = "ListarTipoMedicion")]
    public async Task<IActionResult> ListarTipoMedicion(int id)
    {
        var query = new SelectParameterDetailsQuery() { ParentId = (int)ParameterEnum.MeasurementType };
        return Ok(await mediator.Send(query));
    }
    // [HttpPost]
    // [Route("~/api/gestion/EditarMetas", Name = "UpdateIndicator")]
    // public async Task<IActionResult> UpdateIndicator(UpdateIndicatorCommand request)
    // {
    //     return Ok(await mediator.Send(request));
    // }
}