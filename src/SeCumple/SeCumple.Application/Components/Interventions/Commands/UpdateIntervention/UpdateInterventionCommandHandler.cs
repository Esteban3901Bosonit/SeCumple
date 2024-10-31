using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Components.Interventions.Commands.CreateIntervention;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Commands.UpdateIntervention;

public class UpdateInterventionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateInterventionCommand, ProcessResult<InterventionResponse>>
{
    public async Task<ProcessResult<InterventionResponse>> Handle(UpdateInterventionCommand request,
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
        intervention.SectorIds = string.Join(",", request.iMaeSectores!);
        intervention.SourceIds = string.Join(",", request.iFuente!);
        intervention.RegionTypeId = request.iTipoRegion;
        intervention.RegionIds = string.Join(",", request.iMaeRegion!);
        intervention.ProvinceIds = string.Join(",", request.iMaeProvincia!);
        intervention.DistrictIds = string.Join(",", request.iMaeDistrito!);
        intervention.UbigeoCode = request.cCodigoUbigeo;
        intervention.PriorityId = request.iPrioridad;
        intervention.PCGCode = request.cCodigoPCG;
        intervention.CUI = request.cCUI;
        intervention.InterventionTypeId = request.iTipoIntervencion;
        intervention.SubInterventionTypeId = request.iSubTipoIntervencion;
        intervention.OtherInterventionType = request.cOtroTipoIntervencion!;
        intervention.ModifiedBy = request.iCodUsuarioRegistro;
        
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
                cEstado = intervention.Status.ToString(),
                cEstadoIntervencion = interventionStatus.Name!,
                iTipoIntervencion = intervention.InterventionTypeId,
                iSubTipoIntervencion = intervention.SubInterventionTypeId,
                cCodigoUbigeo = intervention.UbigeoCode,
                iFuente = intervention.SourceIds!.Split(',').Select(int.Parse).ToArray(),
                iPrioridad = intervention.PriorityId,
                cCodigoPCG = intervention.PCGCode,
                cCUI = intervention.CUI
            }
        };
    }
}

public class UpdateInterventionCommand : CreateInterventionCommand, IRequest<ProcessResult<InterventionResponse>>
{
    public int iMovIntervencion { get; set; }
}