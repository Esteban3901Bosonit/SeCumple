using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Notification.Commands;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class NotificationController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("sendEmail", Name = "SendEmail")]
    public async Task<IActionResult> SendEmail(SendEmailCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}