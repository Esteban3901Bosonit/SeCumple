using MediatR;
using SeCumple.Application.Components.Sectors.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Sectors.Commands.CreateSector;

public class CreateSectorCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateSectorCommand, ProcessResult<SectorResponse>>
{
    public async Task<ProcessResult<SectorResponse>> Handle(CreateSectorCommand request,
        CancellationToken cancellationToken)
    {
        var sector = new Sector
        {
            Name = request.cNombre,
            Acronym = request.cSigla,
            Description = request.cDescripcionCorta,
            Status = request.cEstado,
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<Sector>().AddAsync(sector);

        return new ProcessResult<SectorResponse>
        {
            Data = new SectorResponse
            {
                iMaeSector = sector.Id,
                cNombre = sector.Name,
                cDescripcion = sector.Description,
                cSigla = sector.Acronym,
                cEstado = sector.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class CreateSectorCommand : IRequest<ProcessResult<SectorResponse>>
{
    public string cNombre { get; set; }
    public string cDescripcionCorta { get; set; }
    public string cSigla { get; set; }
    public char cEstado { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}