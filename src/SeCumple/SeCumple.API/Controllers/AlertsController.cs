using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Alerts.Commands.CreateAlert;
using SeCumple.Application.Components.Alerts.Commands.DeleteAlert;
using SeCumple.Application.Components.Alerts.Commands.UpdateAlert;
using SeCumple.Application.Components.Alerts.Queries.ListAlerts;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AlertsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/api/configuracion/listarAlertas", Name = "listAlerts")]
    public async Task<IActionResult> ListAlerts(ListAlertQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/configuracion/InsertarAlerta", Name = "CreateAlert")]
    public async Task<IActionResult> CreateAlert([FromBody] CreateAlertCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/configuracion/ActualizarAlerta", Name = "UpdateAlert")]
    public async Task<IActionResult> UpdateAlert([FromBody] UpdateAlertCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/configuracion/EliminarAlerta", Name = "DeleteAlert")]
    public async Task<IActionResult> DeleteAlert([FromBody] DeleteAlertCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}