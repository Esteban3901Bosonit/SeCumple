using MediatR;
using SeCumple.Application.Components.Sectors.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Sectors.Queries.SelectSector;

public class SelectSectorQueryHandler(IUnitOfWork unitOfWork)  : IRequestHandler<SelectSectorQuery, ProcessResult<IReadOnlyList<SectorResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<SectorResponse>>> Handle(SelectSectorQuery request, CancellationToken cancellationToken)
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

public class SelectSectorQuery: IRequest<ProcessResult<IReadOnlyList<SectorResponse>>>
{
    
}