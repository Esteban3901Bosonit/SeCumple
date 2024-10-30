using MediatR;
using SeCumple.Application.Components.Indicators.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Indicators.Commands.CreateIndicator;

public class CreateIndicatorCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateIndicatorCommand, ProcessResult<IndicatorResponse>>
{
    public async Task<ProcessResult<IndicatorResponse>> Handle(CreateIndicatorCommand request,
        CancellationToken cancellationToken)
    {
        var indicatorStatus =
            await unitOfWork.Repository<ParameterDetail>().GetEntityAsync(x => x.Name == "REGISTRADO");
        
        var indicator = new Indicator
        {
            Name = request.cNombre,
            Action = request.cAccion,
            Description = request.cDescripcion,
            InterventionId = request.iMovIntervencion,
            IndicatorTypeId = request.iTipoIndicador,
            MeasureUnit = request.iUnidadMedida,
            ParentId = request.iMovIndicadorPadre,
            CreatedBy = request.iCodUsuarioRegistro,
            Status = '1',
            IndicatorStatusId = indicatorStatus.Id
        };

        await unitOfWork.Repository<Indicator>().AddAsync(indicator);

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

public class CreateIndicatorCommand : IRequest<ProcessResult<IndicatorResponse>>
{
    public string cNombre { get; set; }
    public string cDescripcion { get; set; }
    public int iMovIntervencion { get; set; }
    public int iTipoIndicador { get; set; }
    public int iUnidadMedida { get; set; }
    public string cAccion { get; set; }
    public int? iMovIndicadorPadre { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}