using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Commands.CreateIntervention;

public class CreateInterventionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInterventionCommand, ProcessResult<InterventionResponse>>
{
    public async Task<ProcessResult<InterventionResponse>> Handle(CreateInterventionCommand request,
        CancellationToken cancellationToken)
    {
        var interventionStatus = await unitOfWork.Repository<ParameterDetail>().GetEntityAsync(x => x.Name == "ACTIVO");

        var intervention = new Intervention
        {
            Name = request.cNombre,
            CreatedBy = request.iCodUsuarioRegistro,
            PlanId = request.iDetPlanCumplimiento,
            GuidelineId = request.iMaeLineamiento,
            OrganicUnitId = request.iMaeUnidadOrganica,
            AssignmentStatusId =
                (await unitOfWork.Repository<ParameterDetail>().GetEntityAsync(x => x.Name == "NO ASIGNADO")).Id,
            InterventionStatusId = interventionStatus.Id
        };

        await unitOfWork.Repository<Intervention>().AddAsync(intervention);

        var includes = new List<Expression<Func<Intervention, object>>>
        {
            x => x.GuideLine!,
            x => x.OrganicUnit!,
            x => x.OrganicUnit!.Sector!,
            x => x.GuideLine!.Axis!
        };

        intervention = await unitOfWork.Repository<Intervention>()
            .GetEntityAsync(x => x.Id == intervention.Id, includes);

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

public class CreateInterventionCommand : IRequest<ProcessResult<InterventionResponse>>
{
    public string? cNombre { get; set; }
    public int iCodUsuarioRegistro { get; set; }
    public int iDetPlanCumplimiento { get; set; }
    public int iMaeLineamiento { get; set; }
    public int iMaeUnidadOrganica { get; set; }
}