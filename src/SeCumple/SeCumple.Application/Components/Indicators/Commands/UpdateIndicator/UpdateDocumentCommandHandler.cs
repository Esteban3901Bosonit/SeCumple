using MediatR;
using SeCumple.Application.Components.Indicators.Commands.CreateIndicator;
using SeCumple.Application.Components.Indicators.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Indicators.Commands.UpdateIndicator;

public class UpdateIndicatorCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateIndicatorCommand, ProcessResult<IndicatorResponse>>
{
    public async Task<ProcessResult<IndicatorResponse>> Handle(UpdateIndicatorCommand request,
        CancellationToken cancellationToken)
    {
        var indicator = await unitOfWork.Repository<Indicator>().GetByIdAsync(request.iMovIndicador);

        indicator.Name = request.cNombre;
        indicator.Action = request.cAccion;
        indicator.Description = request.cDescripcion;
        indicator.InterventionId = request.iMovIntervencion;
        indicator.IndicatorTypeId = request.iTipoIndicador;
        indicator.MeasureUnit = request.iUnidadMedida;
        indicator.ParentId = request.iMovIndicadorPadre;
        indicator.ModifiedBy = request.iCodUsuarioRegistro;
        
        var indicatorStatus =
            await unitOfWork.Repository<ParameterDetail>().GetByIdAsync(indicator.IndicatorStatusId);

        await unitOfWork.Repository<Indicator>().UpdateAsync(indicator);

        return new ProcessResult<IndicatorResponse>
        {
            Data = new IndicatorResponse
            {
                iMovIndicador = indicator.Id,
                cNombre = indicator.Name,
                cEstadoIndicador = indicatorStatus.Name!,
                cDescripcion = indicator.Description,
                cAccion = indicator.Action
            }
        };
    }
}

public class UpdateIndicatorCommand : CreateIndicatorCommand
{
    public int iMovIndicador { get; set; }
}