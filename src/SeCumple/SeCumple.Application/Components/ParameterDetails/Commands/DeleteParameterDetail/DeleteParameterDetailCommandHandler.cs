using MediatR;
using SeCumple.Application.Components.ParameterDetails.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.ParameterDetails.Commands.DeleteParameterDetail;

public class DeleteParameterDetailCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteParameterDetailCommand, ProcessResult<ParameterDetailResponse>>
{
    public async Task<ProcessResult<ParameterDetailResponse>> Handle(DeleteParameterDetailCommand request,
        CancellationToken cancellationToken)
    {
        var parameterDetail = await unitOfWork.Repository<ParameterDetail>().GetByIdAsync(request.iDetParameter);
        parameterDetail.Status = '0';
        parameterDetail.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<ParameterDetail>().UpdateAsync(parameterDetail);

        return new ProcessResult<ParameterDetailResponse>
        {
            Data = new ParameterDetailResponse
            {
                cNombre = parameterDetail.Name!,
                iDetParameter = parameterDetail.Id,
                cEstado = parameterDetail.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class DeleteParameterDetailCommand : IRequest<ProcessResult<ParameterDetailResponse>>
{
    public int iDetParameter { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}