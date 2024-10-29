using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Interventions.Commands.CreateIntervention;
using SeCumple.Application.Components.Interventions.Commands.DeleteInverventionCommandHandler;
using SeCumple.Application.Components.Interventions.Commands.EditIntervention;
using SeCumple.Application.Components.Interventions.Queries.ListIntervention;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class InteventionController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/gestion/ListarIntervencion", Name = "ListIntervention")]
    public async Task<IActionResult> ListIntervention(ListInterventionQuery request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPost]
    [Route("~/gestion/insertarIntervencion", Name = "CreateIntervention")]
    public async Task<IActionResult> CreateIntervention(CreateInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPut]
    [Route("~/gestion/EditarIntervencion", Name = "EditIntervention")]
    public async Task<IActionResult> EditIntervention(EditInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpPut]
    [Route("~/gestion/EliminarIntervencion", Name = "DeleteIntervention")]
    public async Task<IActionResult> EditIntervention(DeleteInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }
}