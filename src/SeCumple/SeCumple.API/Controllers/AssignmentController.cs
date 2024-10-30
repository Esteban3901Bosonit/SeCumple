using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Assignments.Commands.CreateAssignment;
using SeCumple.Application.Components.Assignments.Commands.DeleteAssignment;
using SeCumple.Application.Components.Assignments.Commands.UpdateAssignment;
using SeCumple.Application.Components.Assignments.Queries.ListAssignment;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AssignmentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/api/administracion/listarAsignacion", Name = "listAssignment")]
    public async Task<IActionResult> ListAssignment(ListAssignmentsQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/administracion/InsertarAsignacion", Name = "createAssignment")]
    public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/administracion/ActualizarAsignacion", Name = "updateAssignment")]
    public async Task<IActionResult> UpdateAssignment([FromBody] UpdateAssignmentCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost("~/api/administracion/EliminarAsignacion", Name = "deleteAssignment")]
    public async Task<IActionResult> DeleteAssignment([FromBody] DeleteAssignmentCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}