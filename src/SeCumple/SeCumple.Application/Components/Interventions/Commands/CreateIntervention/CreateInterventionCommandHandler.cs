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
            AssignmentStatusId = (await unitOfWork.Repository<ParameterDetail>().GetEntityAsync(x => x.Name == "NO ASIGNADO")).Id,
            InterventionStatusId = interventionStatus?.Id ?? 0,
            SourceIds = request.iFuente != null ? string.Join(",", request.iFuente!) : null,
            RegionType = request.cTipoRegion,
            InterventionTypeId = request.iTipoIntervencion,
            OtherInterventionType = request.cOtroTipoIntervencion!,
            SubInterventionTypeId = request.iSubTipoIntervencion,
            PriorityId = request.iPrioridad,
            CUI = request.cCUI
        };

        await unitOfWork.Repository<Intervention>().AddAsync(intervention);

        // Verificación de nulidad para objLocalizacion
        var interventionLocations = new List<Ubigeo>();
        if (request.objLocalizacion != null)
        {
            foreach (var location in request.objLocalizacion)
            {
                if (location.iMaeRegion == null && location.iMaeProvincia == null && location.iMaeDistrito == null)
                {
                    continue; // Ignorar localizaciones nulas
                }

                var region = await unitOfWork.Repository<Region>().GetEntityAsync(x => x.Id == location.iMaeRegion);
                var province = location.iMaeProvincia != null
                    ? await unitOfWork.Repository<Province>().GetEntityAsync(x => x.Id == location.iMaeProvincia)
                    : null;
                var district = location.iMaeDistrito != null
                    ? await unitOfWork.Repository<District>().GetEntityAsync(x => x.Id == location.iMaeDistrito)
                    : null;

                var ubigeoCode = $"{region?.Ubigeo ?? ""}{province?.Ubigeo ?? ""}{district?.Ubigeo ?? ""}";

                interventionLocations.Add(new Ubigeo
                {
                    InterventionId = intervention.Id,
                    RegionId = location.iMaeRegion,
                    ProvinceId = location.iMaeProvincia,
                    DistrictId = location.iMaeDistrito,
                    Code = ubigeoCode,
                    CreatedBy = request.iCodUsuarioRegistro,
                });
            }
        }

        unitOfWork.Repository<Ubigeo>().AddRange(interventionLocations);

        var includes = new List<Expression<Func<Intervention, object>>>
        {
            x => x.GuideLine!,
            x => x.OrganicUnit!,
            x => x.OrganicUnit!.Sector!,
            x => x.GuideLine!.Axis!,
            x => x.Regions!
        };

        intervention = await unitOfWork.Repository<Intervention>()
            .GetEntityAsync(x => x.Id == intervention.Id, includes);

        return new ProcessResult<InterventionResponse>
        {
            Data = new InterventionResponse
            {
                iMovIntervencion = intervention.Id,
                cNombre = intervention.Name!,
                iMaeEje = intervention.GuideLine?.Axis?.Id ?? 0,
                cNombreEje = intervention.GuideLine?.Axis?.Title ?? string.Empty,
                cNum = intervention.GuideLine?.Axis?.Numeral ?? string.Empty,
                iMaeLineamiento = intervention.GuidelineId,
                cNombreLineamiento = intervention.GuideLine?.Description ?? string.Empty,
                cNumLineamiento = intervention.GuideLine?.Numeral ?? string.Empty,
                iMaeUnidadOrganica = intervention.OrganicUnitId,
                cNombreUnidadOrganica = intervention.OrganicUnit?.Name ?? string.Empty,
                iMaeSector = intervention.OrganicUnit?.SectorId ?? 0,
                cNombreSector = intervention.OrganicUnit?.Sector?.Name ?? string.Empty,
                cEstado = intervention.Status.ToString(),
                cEstadoIntervencion = interventionStatus?.Name ?? string.Empty,
                iTipoIntervencion = intervention.InterventionTypeId,
                iSubTipoIntervencion = intervention.SubInterventionTypeId,
                iFuente = intervention.SourceIds?.Split(',').Select(int.Parse).ToArray(),
                iPrioridad = intervention.PriorityId,
                cCodigoPCG = intervention.PCGCode,
                cCUI = intervention.CUI,
                Ubigeos = intervention.Regions?.Select(x => new RegionInterventionResponse
                {
                    iMaeRegion = x.RegionId,
                    iMaeProvincia = x.ProvinceId,
                    iMaeDistrito = x.DistrictId,
                    cCodigoUbigeo = x.Code!
                }).ToArray() ?? Array.Empty<RegionInterventionResponse>()
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
    public int[]? iFuente { get; set; }
    public string? cTipoRegion { get; set; }
    public RegionRequest[]? objLocalizacion { get; set; }
    public int? iTipoIntervencion { get; set; }
    public string? cOtroTipoIntervencion { get; set; }
    public int? iSubTipoIntervencion { get; set; }
    public int? iPrioridad { get; set; }
    public string? cCodigoPCG { get; set; }
    public string? cCUI { get; set; }
}

public class RegionRequest
{
    public int? iDetUbigeo { get; set; }
    public int iMaeRegion { get; set; }
    public int? iMaeProvincia { get; set; }
    public int? iMaeDistrito { get; set; }
    public int? cCodUbigeo { get; set; }
}