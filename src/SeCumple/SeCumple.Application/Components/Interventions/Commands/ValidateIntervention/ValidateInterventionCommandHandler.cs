using MediatR;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.CrossCutting.Enums;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Commands.ValidateIntervention;

public class ValidateInterventionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ValidateInterventionCommand, ProcessResult<InterventionResponse>>
{
    public async Task<ProcessResult<InterventionResponse>> Handle(ValidateInterventionCommand request,
        CancellationToken cancellationToken)
    {
        var intervention = await unitOfWork.Repository<Intervention>().GetByIdAsync(request.iDetIntervencion);

        intervention.ModifiedBy = request.iCodUsuarioRegistro;

        if (request.iAprobado)
        {
            var interventionStatus =
                intervention.InterventionStatusId = (await unitOfWork.Repository<ParameterDetail>()
                    .GetEntityAsync(x => x.Name == StatusInterventionEnum.Active.GetEnumMemberValue())).Id;

            // todo: eobtener destinos y enviar correo
        }
        else
        {
            // todo: enviar cooreo con la observacion a los destinatarios
        }

        return new ProcessResult<InterventionResponse>()
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
                cEstadoIntervencion = (await unitOfWork.Repository<ParameterDetail>()
                    .GetEntityAsync(x => x.Name == StatusInterventionEnum.Active.GetEnumMemberValue())).Name!,
                iTipoIntervencion = intervention.InterventionTypeId,
                iSubTipoIntervencion = intervention.SubInterventionTypeId,
                iFuente = intervention.SourceIds!.Split(',').Select(int.Parse).ToArray(),
                iPrioridad = intervention.PriorityId,
                cCodigoPCG = intervention.PCGCode,
                cCUI = intervention.CUI,
                Ubigeos = intervention.Regions!.Select(x => new RegionInterventionResponse
                {
                    iMaeRegion = x.RegionId,
                    iMaeProvincia = x.ProvinceId,
                    iMaeDistrito = x.DistrictId,
                    cCodigoUbigeo = x.Code!
                }).ToArray()
            }
        };
    }
}

public class ValidateInterventionCommand : IRequest<ProcessResult<InterventionResponse>>
{
    public int iDetIntervencion { get; set; }
    public bool iAprobado { get; set; }
    public string cObservacion { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}