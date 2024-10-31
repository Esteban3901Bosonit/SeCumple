using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Monitorings.Commads.CreateMonitoring;
using SeCumple.Application.Components.Monitorings.Commads.DeleteMonitoring;
using SeCumple.Application.Components.Monitorings.Commads.UpdateMonitoring;
using SeCumple.Application.Components.Monitorings.Queries.GetPersonByMonitoringId;
using SeCumple.Application.Components.Monitorings.Queries.ListMonitoring;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MonitoringController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/api/seguimiento/listarMonitoreo", Name = "listMonitoring")]
    public async Task<IActionResult> ListMonitoring(ListMonitoringQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/seguimiento/InsertarMonitoreo", Name = "CrearMonitoring")]
    public async Task<IActionResult> CrearMonitoring([FromBody] CreateMonitoringCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/seguimiento/ActualizarMonitoreo", Name = "UpdateMonitoring")]
    public async Task<IActionResult> UpdateMonitoring([FromBody] UpdateMonitoringCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/seguimiento/EliminarMonitoreo", Name = "DeleteMonitoring")]
    public async Task<IActionResult> DeleteMonitoring([FromBody] DeleteMonitoringCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpGet("~/api/seguimiento/ParticipantesMonitoreo", Name = "GetPersonByMonitoringId")]
    public async Task<IActionResult> GetPersonByMonitoringId(int id)
    {
        var query = new GetPersonByMonitoringIdQuery { MonitoringId = id };
        return Ok(await mediator.Send(query));
    }
}