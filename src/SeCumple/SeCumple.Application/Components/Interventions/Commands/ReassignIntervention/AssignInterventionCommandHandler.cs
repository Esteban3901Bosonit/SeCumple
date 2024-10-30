using MediatR;
using SeCumple.Application.Components.Interventions.Commands.AssignIntervention;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Commands.ReassignIntervention;

public class ReassignInterventionCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    : IRequestHandler<ReassignInterventionCommand, ProcessResult<InterventionResponse>>
{
    public async Task<ProcessResult<InterventionResponse>> Handle(ReassignInterventionCommand request,
        CancellationToken cancellationToken)
    {
        var interventionAssignmentsToDelete = await unitOfWork.Repository<InterventionAssignment>()
            .GetAsync(x => x.InterventionId == request.iMovIntervencion);

        foreach (var interventionAssignmentToDelete in interventionAssignmentsToDelete)
        {
            interventionAssignmentToDelete.Status = '0';
            interventionAssignmentToDelete.ModifiedBy = request.iCodUsuarioRegistro;
            await unitOfWork.Repository<InterventionAssignment>().UpdateAsync(interventionAssignmentToDelete);
        }

        var assignInterventionCommand = new AssignInterventionCommand
        {
            iCodUsuarioRegistro = request.iCodUsuarioRegistro,
            iMovIntervencion = request.iMovIntervencion,
            iMovAsigAsesorSector = request.iMovAsigAsesorSector,
            iMovAsigCoordinadorOCG = request.iMovAsigCoordinadorOCG,
            iMovAsigEspecialistaOCG = request.iMovAsigEspecialistaOCG,
            iMovAsigEspecialistaSector = request.iMovAsigEspecialistaSector
        };

        return await mediator.Send(assignInterventionCommand, cancellationToken);
    }
}

public class ReassignInterventionCommand : AssignInterventionCommand
{
}