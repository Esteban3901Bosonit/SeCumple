using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Components.GuideLines.Dtos;
using SeCumple.Application.Components.GuideLines.Queries.ListGuideLines;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Queries.ListLocations;

public class ListLocationsHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListLocationsQuery, ProcessResult<IReadOnlyList<RegionResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<RegionResponse>>> Handle(ListLocationsQuery request,
        CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Region, object>>>
        {
            x => x.Provinces
        };

        // Obtenemos las regiones incluyendo las provincias y distritos
        var regions = await unitOfWork.Repository<Region>()
            .GetAsync(x => x.Status == '1', null, includes);

        // Mapeamos las regiones a RegionResponse
        var regionResponse = regions.Select(region => new RegionResponse
        {
            iMaeRegion = region.Id,
            cNombre = region.Name,
            cUbigeo = region.Ubigeo,
            Provinces = region.Provinces?.Select(province => new ProvinceResponse
            {
                iMaeProvincia = province.Id,
                cNombre = province.Name,
                cUbigeo = province.Ubigeo,
                // Aquí ahora hacemos la selección de Distritos
                Districts = province.Districts?.Select(district => new DistrictResponse
                {
                    iMaeDistrito = district.Id,
                    cNombre = district.Name,
                    cUbigeo = district.Ubigeo
                }).ToList()
            }).ToList()
        }).ToList();
        
        return new ProcessResult<IReadOnlyList<RegionResponse>>()
        {
            Data = regionResponse
        };
    }
}

public class ListLocationsQuery : IRequest<ProcessResult<IReadOnlyList<RegionResponse>>>
{
    public int AxisId { get; set; }
}