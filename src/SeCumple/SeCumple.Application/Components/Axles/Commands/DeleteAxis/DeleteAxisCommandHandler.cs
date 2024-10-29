using MediatR;
using SeCumple.Application.Components.Axles.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Axles.Commands.DeleteAxis;

public class DeleteAxisCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAxisCommand, ProcessResult<AxisResponse>>
{
    public async Task<ProcessResult<AxisResponse>> Handle(DeleteAxisCommand request,
        CancellationToken cancellationToken)
    {
        var axis = await unitOfWork.Repository<Axis>().GetByIdAsync(request.iMaeEje);

        axis.Status = '0';
        axis.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Axis>().UpdateAsync(axis);

        return new ProcessResult<AxisResponse>
        {
            Data = new AxisResponse
            {
                iMaeEje = axis.Id,
                cEstado = axis.Status == '1' ? "SI" : "NO",
                cNum = axis.Numeral!,
                cTitulo = axis.Title!,
                cValidado = axis.Validated!,
                cTipoRegistro = "Eje"
            }
        };
    }
}

public class DeleteAxisCommand : IRequest<ProcessResult<AxisResponse>>
{
    public int iMaeEje { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}