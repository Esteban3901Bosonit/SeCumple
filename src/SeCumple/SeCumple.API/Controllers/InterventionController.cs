using MediatR;
using Microsoft.AspNetCore.Mvc;
using SeCumple.Application.Components.Interventions.Commands.AssignIntervention;
using SeCumple.Application.Components.Interventions.Commands.CreateIntervention;
using SeCumple.Application.Components.Interventions.Commands.DeleteInverventionCommandHandler;
using SeCumple.Application.Components.Interventions.Commands.ReassignIntervention;
using SeCumple.Application.Components.Interventions.Commands.UpdateIntervention;
using SeCumple.Application.Components.Interventions.Queries.ListIntervention;
using SeCumple.Application.Components.Interventions.Queries.ListLocations;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class InterventionController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("~/api/gestion/ListarIntervencion", Name = "ListIntervention")]
    public async Task<IActionResult> ListIntervention(ListInterventionQuery request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/insertarIntervencion", Name = "CreateIntervention")]
    public async Task<IActionResult> CreateIntervention(CreateInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/EditarIntervencion", Name = "updateIntervention")]
    public async Task<IActionResult> UpdateIntervention(UpdateInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/EliminarIntervencion", Name = "DeleteIntervention")]
    public async Task<IActionResult> EditIntervention(DeleteInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/AsignarIntervencion", Name = "AssignIntervention")]
    public async Task<IActionResult> AssignIntervention(AssignInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }

    [HttpPost]
    [Route("~/api/gestion/ReAsignarIntervencion", Name = "ReassignIntervention")]
    public async Task<IActionResult> ReassignIntervention(ReassignInterventionCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    
    [HttpGet]
    [Route("~/api/configuracion/Localizaciones", Name = "listLocations")]
    public async Task<IActionResult> ListLocations()
    {
        var query = new ListLocationsQuery();
        return Ok(await mediator.Send(query));
    }
}