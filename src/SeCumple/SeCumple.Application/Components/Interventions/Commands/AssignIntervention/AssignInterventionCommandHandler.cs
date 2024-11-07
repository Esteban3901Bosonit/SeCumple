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
        var assignments = request.iMovAsigEspecialistaOCG.Select(id => new InterventionAssignment
        {
            CreatedBy = request.iCodUsuarioRegistro,
            InterventionId = request.iMovIntervencion,
            AssigmentId = id
        }).ToList();
        
        assignments.AddRange(request.iMovAsigCoordinadorOCG.Select(id => new InterventionAssignment
        {
            CreatedBy = request.iCodUsuarioRegistro,
            InterventionId = request.iMovIntervencion,
            AssigmentId = id
        }));
        
        assignments.AddRange(request.iMovAsigAsesorSector.Select(id => new InterventionAssignment
        {
            CreatedBy = request.iCodUsuarioRegistro,
            InterventionId = request.iMovIntervencion,
            AssigmentId = id
        }));
        
        assignments.AddRange(request.iMovAsigEspecialistaSector.Select(id => new InterventionAssignment
        {
            CreatedBy = request.iCodUsuarioRegistro,
            InterventionId = request.iMovIntervencion,
            AssigmentId = id
        }));

        unitOfWork.Repository<InterventionAssignment>().AddRange(assignments);

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
                iMaeSector = intervention.OrganicUnit!.SectorId!,
                cNombreSector = intervention.OrganicUnit!.Sector!.Name!,
                cEstado = assignmentStatus.Name!
            }
        };
    }
}

public class AssignInterventionCommand : IRequest<ProcessResult<InterventionResponse>>
{
    public int iMovIntervencion { get; set; }
    public int[] iMovAsigEspecialistaOCG { get; set; }
    public int[] iMovAsigCoordinadorOCG { get; set; }
    public int[] iMovAsigEspecialistaSector { get; set; }
    public int[] iMovAsigAsesorSector { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}