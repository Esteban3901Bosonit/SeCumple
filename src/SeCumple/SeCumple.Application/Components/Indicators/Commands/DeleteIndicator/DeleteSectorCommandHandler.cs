using MediatR;
using SeCumple.Application.Components.Indicators.Dtos;
using SeCumple.Application.Components.OrganicUnits.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Indicators.Commands.DeleteIndicator;

public class DeleteIndicatorCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteIndicatorCommand, ProcessResult<IndicatorResponse>>
{
    public async Task<ProcessResult<IndicatorResponse>> Handle(DeleteIndicatorCommand request,
        CancellationToken cancellationToken)
    {
        var indicator = await unitOfWork.Repository<Indicator>().GetByIdAsync(request.iMovIndicador);
        indicator.Status = '0';
        indicator.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Indicator>().UpdateAsync(indicator);

        return new ProcessResult<IndicatorResponse>
        {
            Data = new IndicatorResponse
            {
                iMovIndicador = indicator.Id,
                cNombre = indicator.Name!,
                cDescripcion = indicator.Description,
                cAccion = indicator.Action!
            }
        };
    }
}

public class DeleteIndicatorCommand : IRequest<ProcessResult<IndicatorResponse>>
{
    public int iMovIndicador { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}