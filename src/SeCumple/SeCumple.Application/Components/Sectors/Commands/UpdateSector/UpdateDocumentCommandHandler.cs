using MediatR;
using SeCumple.Application.Components.Sectors.Commands.CreateSector;
using SeCumple.Application.Components.Sectors.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Sectors.Commands.UpdateSector;

public class UpdateSectorCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateSectorCommand, ProcessResult<SectorResponse>>
{
    public async Task<ProcessResult<SectorResponse>> Handle(UpdateSectorCommand request,
        CancellationToken cancellationToken)
    {
        var sector = await unitOfWork.Repository<Sector>().GetByIdAsync(request.iMaeSector);

        sector.Name = request.cNombre;
        sector.Description = request.cDescripcionCorta;
        sector.Acronym = request.cSigla;
        sector.Status = request.cEstado;
        sector.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Sector>().UpdateAsync(sector);

        return new ProcessResult<SectorResponse>
        {
            Data = new SectorResponse
            {
                cNombre = sector.Name,
                iMaeSector = sector.Id,
                cSigla = sector.Acronym,
                cDescripcion = sector.Description,
                cEstado = sector.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class UpdateSectorCommand : CreateSectorCommand
{
    public int iMaeSector { get; set; }
}