using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Components.Interventions.Commands.CreateIntervention;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Commands.EditIntervention;

public class EditInterventionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<EditInterventionCommand, ProcessResult<InterventionResponse>>
{
    public async Task<ProcessResult<InterventionResponse>> Handle(EditInterventionCommand request,
        CancellationToken cancellationToken)
    {
        
        var includes = new List<Expression<Func<Intervention, object>>>
        {
            x => x.GuideLine!,
            x => x.OrganicUnit!,
            x => x.OrganicUnit!.Sector!,
            x => x.GuideLine!.Axis!
        };

        var intervention = await unitOfWork.Repository<Intervention>()
            .GetEntityAsync(x => x.Id == request.iMovIntervencion, includes);

        var interventionStatus =
            await unitOfWork.Repository<ParameterDetail>().GetByIdAsync(intervention.InterventionStatusId);

        intervention.ModifiedBy=request.iCodUsuarioRegistro;
        intervention.Name = request.cNombre;
        intervention.GuidelineId = request.iMaeLineamiento;
        intervention.OrganicUnitId = request.iMaeUnidadOrganica;
        intervention.PlanId = request.iDetPlanCumplimiento;
        
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
                cEstado = interventionStatus.Name!
            }
        };
    }
}

public class EditInterventionCommand : CreateInterventionCommand, IRequest<ProcessResult<InterventionResponse>>
{
    public int iMovIntervencion { get; set; }
}