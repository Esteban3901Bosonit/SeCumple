using MediatR;
using SeCumple.Application.Components.Sectors.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Sectors.Commands.DeleteSector;

public class DeleteSectorCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSectorCommand, ProcessResult<SectorResponse>>
{
    public async Task<ProcessResult<SectorResponse>> Handle(DeleteSectorCommand request,
        CancellationToken cancellationToken)
    {
        var sector = await unitOfWork.Repository<Sector>().GetByIdAsync(request.iMaeSector);
        sector.Status = '0';
        sector.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Sector>().UpdateAsync(sector);

        return new ProcessResult<SectorResponse>
        {
            Data = new SectorResponse
            {
                cNombre = sector.Name!,
                iMaeSector = sector.Id,
                cSigla = sector.Acronym!,
                cEstado = sector.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class DeleteSectorCommand : IRequest<ProcessResult<SectorResponse>>
{
    public int iMaeSector { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}