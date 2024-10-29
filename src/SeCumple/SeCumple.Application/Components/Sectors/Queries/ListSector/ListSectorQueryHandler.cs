using MediatR;
using SeCumple.Application.Components.Sectors.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Sectors.Queries.ListSector;

public class ListSectorQueryHandler(IUnitOfWork unitOfWork)  : IRequestHandler<ListSectorQuery, ProcessResult<IReadOnlyList<SectorResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<SectorResponse>>> Handle(ListSectorQuery request, CancellationToken cancellationToken)
    {
        var sectors = await unitOfWork.Repository<Sector>().GetAsync(x => x.Status == '1');

        return new ProcessResult<IReadOnlyList<SectorResponse>>
        {
            Data = sectors.Select(s => new SectorResponse
            {
                iMaeSector = s.Id,
                cNombre = s.Name!,
                cSigla = s.Acronym!,
                cEstado = s.Status.ToString()
            }).ToList()
        };
    }
}

public class ListSectorQuery: IRequest<ProcessResult<IReadOnlyList<SectorResponse>>>
{
    
}