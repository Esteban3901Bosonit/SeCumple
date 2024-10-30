using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Indicators.Commands.CreateIndicator;
using SeCumple.Application.Components.Indicators.Commands.DeleteIndicator;
using SeCumple.Application.Components.Indicators.Commands.UpdateIndicator;
using SeCumple.Application.Components.Indicators.Queries.ListIndicator;
using SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;
using SeCumple.CrossCutting.Enums;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class IndicatorController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("~/configuracion/listarTipoIndicador", Name = "SelectIndicatorTypes")]
    public async Task<IActionResult> SelectIndicatorTypes()
    {
        var query = new SelectParameterDetailsQuery { ParentId = (int)ParameterEnum.IndicatorType };
        return Ok(await mediator.Send(query));
    }

    [HttpGet]
    [Route("~/configuracion/ListarTipoMedicion", Name = "SelectMeasurementTypes")]
    public async Task<IActionResult> SelectMeasurementTypes()
    {
        var query = new SelectParameterDetailsQuery { ParentId = (int)ParameterEnum.MeasurementType };
        return Ok(await mediator.Send(query));
    }

    [HttpGet]
    [Route("~/configuracion/ListarPeriodicidad", Name = "SelectPerioricity")]
    public async Task<IActionResult> SelectPerioricity()
    {
        var query = new SelectParameterDetailsQuery { ParentId = (int)ParameterEnum.Periodicity };
        return Ok(await mediator.Send(query));
    }

    [HttpPost]
    [Route("~/api/gestion/listarIndicador", Name = "listIndicator")]
    public async Task<IActionResult> ListIndicator(ListIndicatorQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/InsertarIndicador", Name = "CreateIndicator")]
    public async Task<IActionResult> CreateIndicator(CreateIndicatorCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/EditarIndicador", Name = "UpdateIndicator")]
    public async Task<IActionResult> UpdateIndicator(UpdateIndicatorCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/EliminarIndicador", Name = "DeleteIndicator")]
    public async Task<IActionResult> DeleteIndicator(DeleteIndicatorCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}