using MediatR;
using SeCumple.Application.Components.ParameterDetails.Commands.CreateParameterDetail;
using SeCumple.Application.Components.ParameterDetails.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.ParameterDetails.Commands.UpdateParameterDetail;

public class UpdateParameterDetailCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateParameterDetailCommand, ProcessResult<ParameterDetailResponse>>
{
    public async Task<ProcessResult<ParameterDetailResponse>> Handle(UpdateParameterDetailCommand request,
        CancellationToken cancellationToken)
    {
        var parameterDetail = await unitOfWork.Repository<ParameterDetail>().GetByIdAsync(request.iDetParameter);

        parameterDetail.Name = request.cNombre;
        parameterDetail.ParentId = request.iMaeParameter;
        parameterDetail.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<ParameterDetail>().UpdateAsync(parameterDetail);

        return new ProcessResult<ParameterDetailResponse>
        {
            Data = new ParameterDetailResponse
            {
                cNombre = parameterDetail.Name,
                iDetParameterId = parameterDetail.Id,
                cEstado = parameterDetail.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public abstract class UpdateParameterDetailCommand : CreateParameterDetailCommand
{
    public int iDetParameter { get; set; }
}