using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Devices.Queries.ListDevice;

namespace SeCumple.API.Controllers;

public class DeviceController(IMediator mediator) : ControllerBase
{
    [HttpGet("list", Name = "ListDevices")]
    public async Task<IActionResult> ListDevices()
    {
        var query = new ListDeviceCommand();
        return Ok(await mediator.Send(query));
    }
}