using MediatR;
using SeCumple.Application.Components.OrganicUnits.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.OrganicUnits.Commands.CreateOrganicUnit;

public class CreateOrganicUnitCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOrganicUnitCommand, ProcessResult<OrganicUnitResponse>>
{
    public async Task<ProcessResult<OrganicUnitResponse>> Handle(CreateOrganicUnitCommand request,
        CancellationToken cancellationToken)
    {
        var organicUnit = new OrganicUnit
        {
            Name = request.cNombre,
            Acronym = request.cSigla,
            SectorId = request.iMaeSector,
            Status = request.cEstado,
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<OrganicUnit>().AddAsync(organicUnit);

        return new ProcessResult<OrganicUnitResponse>
        {
            Data = new OrganicUnitResponse
            {
                iMaeSector = organicUnit.SectorId,
                cNombre = organicUnit.Name,
                cSigla = organicUnit.Acronym,
                iMaeUnidadOrganica = organicUnit.Id,
                cEstado = organicUnit.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class CreateOrganicUnitCommand : IRequest<ProcessResult<OrganicUnitResponse>>
{
    public int iMaeSector { get; set; }
    public string cNombre { get; set; }
    public string cSigla { get; set; }
    public char cEstado { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}