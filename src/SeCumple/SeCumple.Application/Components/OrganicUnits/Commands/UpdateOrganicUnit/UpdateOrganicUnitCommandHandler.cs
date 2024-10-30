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
        var organicUnit = await unitOfWork.Repository<OrganicUnit>().GetByIdAsync(request.iMaeOrganicUnit);

        organicUnit.Name = request.cNombre;
        organicUnit.Acronym = request.cSigla;
        organicUnit.Status = request.cEstado;
        organicUnit.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<OrganicUnit>().UpdateAsync(organicUnit);

        return new ProcessResult<OrganicUnitResponse>
        {
            Data = new OrganicUnitResponse
            {
                cNombre = organicUnit.Name,
                iMaeSector = organicUnit.Id,
                cSigla = organicUnit.Acronym,
                cEstado = organicUnit.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public abstract class UpdateOrganicUnitCommand : CreateOrganicUnitCommand
{
    public int iMaeOrganicUnit { get; set; }
}