using MediatR;
using SeCumple.Application.Components.OrganicUnits.Commands.CreateOrganicUnit;
using SeCumple.Application.Components.OrganicUnits.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.OrganicUnits.Commands.UpdateOrganicUnit;

public class UpdateOrganicUnitCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateOrganicUnitCommand, ProcessResult<OrganicUnitResponse>>
{
    public async Task<ProcessResult<OrganicUnitResponse>> Handle(UpdateOrganicUnitCommand request,
        CancellationToken cancellationToken)
    {
        var sector = await unitOfWork.Repository<OrganicUnit>().GetByIdAsync(request.iMaeOrganicUnit);

        sector.Name = request.cNombre;
        sector.Acronym = request.cSigla;
        sector.Status = request.cEstado;
        sector.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<OrganicUnit>().UpdateAsync(sector);

        return new ProcessResult<OrganicUnitResponse>
        {
            Data = new OrganicUnitResponse
            {
                cNombre = sector.Name,
                iMaeSector = sector.Id,
                cSigla = sector.Acronym,
                cEstado = sector.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public abstract class UpdateOrganicUnitCommand : CreateOrganicUnitCommand
{
    public int iMaeOrganicUnit { get; set; }
}