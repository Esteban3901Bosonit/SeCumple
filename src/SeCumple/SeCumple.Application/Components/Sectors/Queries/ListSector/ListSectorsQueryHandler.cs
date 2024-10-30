using MediatR;
using SeCumple.Application.Components.Sectors.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Sectors;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Sectors.Queries.ListSector;

public class ListSectorsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListSectorsQuery, ProcessResult<PaginationResponse<SectorResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<SectorResponse>>> Handle(ListSectorsQuery request,
        CancellationToken cancellationToken)
    {
        var sectorSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var sectorSpec = new SectorSpecification(sectorSpecParams);
        var sectors = await unitOfWork.Repository<Sector>().GetAllWithSpec(sectorSpec);

        var specCount = new SectorForCountingSpecification(sectorSpecParams);
        var totalSectors = await unitOfWork.Repository<Sector>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalSectors) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var sectorResponse = sectors.Select(d => new SectorResponse
        {
            iMaeSector = d.Id,
            cNombre = d.Name!,
            cSigla = d.Acronym!,
            cDescripcion = d.Description!,
            cEstado = d.Status == '1' ? "ACTIVO" : "INACTIVO"
        });

        return new ProcessResult<PaginationResponse<SectorResponse>>
        {
            Data = new PaginationResponse<SectorResponse>
            {
                Count = totalSectors,
                Data = sectorResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = sectors.Count
            }
        };
    }
}

public class ListSectorsQuery : PaginationRequest,
    IRequest<ProcessResult<PaginationResponse<SectorResponse>>>
{
}