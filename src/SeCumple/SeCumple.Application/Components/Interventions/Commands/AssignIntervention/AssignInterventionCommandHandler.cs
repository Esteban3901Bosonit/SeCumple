using MediatR;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Commands.AssignIntervention;

public class AssignInterventionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AssignInterventionCommand, ProcessResult<InterventionResponse>>
{
    public async Task<ProcessResult<InterventionResponse>> Handle(AssignInterventionCommand request,
        CancellationToken cancellationToken)
    {
        var interventionAssignment = new InterventionAssignment
        {
            CreatedBy = request.iCodUsuarioRegistro,
            InterventionId = request.iMovIntervencion,
            AssigmentId = request.iMovAsigEspecialistaOCG
        };
        await unitOfWork.Repository<InterventionAssignment>().AddAsync(interventionAssignment);

        interventionAssignment.AssigmentId = request.iMovAsigCoordinadorOCG;
        await unitOfWork.Repository<InterventionAssignment>().AddAsync(interventionAssignment);

        interventionAssignment.AssigmentId = request.iMovAsigEspecialistaSector;
        await unitOfWork.Repository<InterventionAssignment>().AddAsync(interventionAssignment);

        interventionAssignment.AssigmentId = request.iMovAsigAsesorSector;
        await unitOfWork.Repository<InterventionAssignment>().AddAsync(interventionAssignment);

        var assignmentStatus =
            await unitOfWork.Repository<ParameterDetail>().GetEntityAsync(x => x.Name == "ASIGNADO");

        var intervention = await unitOfWork.Repository<Intervention>()
            .GetEntityAsync(x => x.Id == request.iMovIntervencion);

        intervention.AssignmentStatusId = assignmentStatus.Id;

        await unitOfWork.Repository<Intervention>().UpdateAsync(intervention);

        return new ProcessResult<InterventionResponse>
        {
            Data = new InterventionResponse
            {
                iMovIntervencion = intervention.Id,
                cNombre = intervention.Name!,
                iMaeEje = intervention.GuideLine!.Axis!.Id!,
                cNombreEje = intervention.GuideLine!.Axis!.Title!,
                cNum = intervention.GuideLine!.Axis!.Numeral!,
                iMaeLineamiento = intervention.GuidelineId,
                cNombreLineamiento = intervention.GuideLine!.Description!,
                cNumLineamiento = intervention.GuideLine!.Numeral!,
                iMaeUnidadOrganica = intervention.OrganicUnitId,
                cNombreUnidadOrganica = intervention.OrganicUnit!.Name!,
                IMaeSector = intervention.OrganicUnit!.SectorId!,
                cNombreSector = intervention.OrganicUnit!.Sector!.Name!,
                cEstado = assignmentStatus.Name!
            }
        };
    }
}

public class AssignInterventionCommand : IRequest<ProcessResult<InterventionResponse>>
{
    public int iMovIntervencion { get; set; }
    public int iMovAsigEspecialistaOCG { get; set; }
    public int iMovAsigCoordinadorOCG { get; set; }
    public int iMovAsigEspecialistaSector { get; set; }
    public int iMovAsigAsesorSector { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}