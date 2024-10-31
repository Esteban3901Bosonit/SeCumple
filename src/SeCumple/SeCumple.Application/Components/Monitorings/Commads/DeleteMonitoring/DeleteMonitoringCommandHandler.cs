using MediatR;
using SeCumple.Application.Components.Alerts.Commands.DeleteAlert;
using SeCumple.Application.Components.Alerts.Dtos;
using SeCumple.Application.Components.Monitorings.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Monitorings.Commads.DeleteMonitoring;

public class DeleteMonitoringCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteMonitoringCommand, ProcessResult<MonitoringResponse>>
{
    public async Task<ProcessResult<MonitoringResponse>> Handle(DeleteMonitoringCommand request,
        CancellationToken cancellationToken)
    {
        var monitoring = await unitOfWork.Repository<Monitoring>().GetByIdAsync(request.iMovMonitoreo);

        monitoring.Status = '0';
        monitoring.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Monitoring>().UpdateAsync(monitoring);

        return new ProcessResult<MonitoringResponse>
        {
            Data = new MonitoringResponse
            {
                iMovMonitoreo = monitoring.Id,
                iMovIntervencion = monitoring.InterventionId,
                iTipoMonitoreo = monitoring.MonitoringTypeId,
                cAsunto = monitoring.Topic,
                dFechaInicio = monitoring.StartDate,
                dFechaFin = monitoring.EndDate,
                cLugar = monitoring.Room,
                iPrioridadMonitoreo = monitoring.MonitoringPriorityId,
                iAprobado = monitoring.Approved,
                cEstado = "Cancelado"
            }
        };
    }
}

public class DeleteMonitoringCommand : IRequest<ProcessResult<MonitoringResponse>>
{
    public int iMovMonitoreo { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}