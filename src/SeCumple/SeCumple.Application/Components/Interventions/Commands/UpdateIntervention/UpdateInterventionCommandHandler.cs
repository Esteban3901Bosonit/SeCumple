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

        intervention.ModifiedBy = request.iCodUsuarioRegistro;
        intervention.Name = request.cNombre;
        intervention.GuidelineId = request.iMaeLineamiento;
        intervention.OrganicUnitId = request.iMaeUnidadOrganica;
        intervention.PlanId = request.iDetPlanCumplimiento;
        intervention.SourceIds = string.Join(",", request.iFuente!);
        intervention.RegionType = request.cTipoRegion;
        intervention.PriorityId = request.iPrioridad;
        intervention.PCGCode = request.cCodigoPCG;
        intervention.CUI = request.cCUI;
        intervention.InterventionTypeId = request.iTipoIntervencion;
        intervention.SubInterventionTypeId = request.iSubTipoIntervencion;
        intervention.OtherInterventionType = request.cOtroTipoIntervencion!;
        intervention.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Intervention>().UpdateAsync(intervention);

        var locationToRemove = intervention.Regions
            ?.Where(x => !request.objLocalizacion!.Select(x => x.iDetUbigeo).Contains(x.Id)).ToList();

        foreach (var location in locationToRemove)
        {
            location.Status = '0';
            location.ModifiedBy = request.iCodUsuarioRegistro;
            await unitOfWork.Repository<Ubigeo>().UpdateAsync(location);
        }

        foreach (var item in request.objLocalizacion!.Where(x => x.iDetUbigeo > 0))
        {
            var location = await unitOfWork.Repository<Ubigeo>()
                .GetEntityAsync(x => x.Id == item.iDetUbigeo && (x.RegionId != item.iMaeRegion
                                                                 || x.ProvinceId != item.iMaeProvincia
                                                                 || x.DistrictId != item.iMaeDistrito));
            if (location != null)
            {
                var region = await unitOfWork.Repository<Region>().GetEntityAsync(x => x.Id == item.iMaeRegion);
                var province = await unitOfWork.Repository<Province>().GetEntityAsync(x => x.Id == item.iMaeProvincia);
                var district = await unitOfWork.Repository<District>().GetEntityAsync(x => x.Id == item.iMaeDistrito);
                var ubigeoCode = $"{region.Ubigeo}{province.Ubigeo ?? ""}{district.Ubigeo ?? ""}";
                location.RegionId = item.iMaeRegion;
                location.ProvinceId = item.iMaeProvincia;
                location.DistrictId = item.iMaeDistrito;
                location.Code = ubigeoCode;
                location.ModifiedBy = request.iCodUsuarioRegistro;
                await unitOfWork.Repository<Ubigeo>().UpdateAsync(location);
            }
        }

        var newLocations = new List<Ubigeo>();
        foreach (var item in request.objLocalizacion!.Where(x => x is { iMaeRegion: > 0, iDetUbigeo: null }))
        {
            var region = await unitOfWork.Repository<Region>().GetEntityAsync(x => x.Id == item.iMaeRegion);
            var province = await unitOfWork.Repository<Province>().GetEntityAsync(x => x.Id == item.iMaeProvincia);
            var district = await unitOfWork.Repository<District>().GetEntityAsync(x => x.Id == item.iMaeDistrito);
            var ubigeoCode = $"{region.Ubigeo}{province.Ubigeo ?? ""}{district.Ubigeo ?? ""}";

            newLocations.Add(new Ubigeo
            {
                InterventionId = intervention.Id,
                RegionId = item.iMaeRegion,
                ProvinceId = item.iMaeProvincia,
                DistrictId = item.iMaeDistrito,
                Code = ubigeoCode,
                CreatedBy = request.iCodUsuarioRegistro
            });
        }

        unitOfWork.Repository<Ubigeo>().AddRange(newLocations);

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

public class UpdateInterventionCommand : CreateInterventionCommand, IRequest<ProcessResult<InterventionResponse>>
{
    public int iMovIntervencion { get; set; }
}