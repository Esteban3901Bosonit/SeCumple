using MediatR;
using SeCumple.Application.Components.OrganicUnits.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.CrossCutting.Exceptions;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.OrganicUnits.Commands.DeleteOrganicUnit;

public class DeleteOrganicUnitCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteOrganicUnitCommand, ProcessResult<OrganicUnitResponse>>
{
    public async Task<ProcessResult<OrganicUnitResponse>> Handle(DeleteOrganicUnitCommand request,
        CancellationToken cancellationToken)
    {
        var organicUnit = await unitOfWork.Repository<OrganicUnit>().GetByIdAsync(request.iMaeOrganicUnit);
        if (organicUnit == null)
        {
            throw new Exception("Unidad Organica no existe");
        }
        organicUnit.Status = '0';
        organicUnit.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<OrganicUnit>().UpdateAsync(organicUnit);

        return new ProcessResult<OrganicUnitResponse>
        {
            Data = new OrganicUnitResponse
            {
                cNombre = organicUnit.Name!,
                iMaeSector = organicUnit.Id,
                cSigla = organicUnit.Acronym!,
                cEstado = organicUnit.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class DeleteOrganicUnitCommand : IRequest<ProcessResult<OrganicUnitResponse>>
{
    public int iMaeOrganicUnit { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}