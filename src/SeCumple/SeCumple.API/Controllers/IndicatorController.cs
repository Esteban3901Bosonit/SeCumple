using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Indicators.Queries.ListIndicator;
using SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;
using SeCumple.CrossCutting.Enums;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class IndicatorController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/api/gestion/listarIndicador", Name = "list")]
    public async Task<IActionResult> ListIndicator(ListIndicatorQuery request)
    {
        return Ok(await mediator.Send(request));
    }
    
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
}