using MediatR;
using SeCumple.Application.Components.Axles.Commands.CreateAxis;
using SeCumple.Application.Components.Axles.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Axles.Commands.UpdateAxis;

public class UpdateAxisCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAxisCommand, ProcessResult<AxisResponse>>
{
    public async Task<ProcessResult<AxisResponse>> Handle(UpdateAxisCommand request,
        CancellationToken cancellationToken)
    {
        var axis = await unitOfWork.Repository<Axis>().GetByIdAsync(request.iMaeEje);

        axis.Description = request.cDescripcion;
        axis.Title = request.cTitulo;
        axis.ModifiedBy = request.iCodUsuarioRegistro;
        axis.Numeral = request.cNum;
        axis.DocumentId = request.iMaeDispositivo;

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

public class UpdateAxisCommand : CreateAxisCommand
{
    public int iMaeEje { get; set; }
}