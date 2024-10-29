using MediatR;
using SeCumple.Application.Components.OrganicUnits.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.OrganicUnits.Queries.ListOrganicUnit;

public class GetOrganicUnitBySectorIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOrganicUnitBySectorIdQuery, ProcessResult<IReadOnlyList<OrganicUnitResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<OrganicUnitResponse>>> Handle(GetOrganicUnitBySectorIdQuery request,
        CancellationToken cancellationToken)
    {
        var organicUnits = await unitOfWork.Repository<OrganicUnit>().GetAsync(
            x => x.Status == '1' && x.SectorId == request.SectorId);

        return new ProcessResult<IReadOnlyList<OrganicUnitResponse>>
        {
            Data = organicUnits.Select(a => new OrganicUnitResponse
            {
                cEstado = a.Status.ToString(),
                cNombre = a.Name!,
                cSigla = a.Acronym!,
                iMaeUnidadOrganica = a.Id,
                iMaeSector = a.SectorId
            }).ToList()
        };
    }
}

public class GetOrganicUnitBySectorIdQuery : IRequest<ProcessResult<IReadOnlyList<OrganicUnitResponse>>>
{
    public int SectorId { get; set; }
}