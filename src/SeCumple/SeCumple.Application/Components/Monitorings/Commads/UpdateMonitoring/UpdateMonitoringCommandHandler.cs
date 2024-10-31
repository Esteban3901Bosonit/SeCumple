using MediatR;
using SeCumple.Application.Components.Monitorings.Commads.CreateMonitoring;
using SeCumple.Application.Components.Monitorings.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Monitorings.Commads.UpdateMonitoring;

public class UpdateMonitoringCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateMonitoringCommand, ProcessResult<MonitoringResponse>>
{
    public async Task<ProcessResult<MonitoringResponse>> Handle(UpdateMonitoringCommand request,
        CancellationToken cancellationToken)
    {
        var monitoring = await unitOfWork.Repository<Monitoring>().GetByIdAsync(request.iMovMonitoreo);

        monitoring.InterventionId = request.iMovIntervencion;
        monitoring.StartDate = request.dFechaInicio;
        monitoring.EndDate = request.dFechaFin;
        monitoring.Room = request.cLugar;
        monitoring.Topic = request.cAsunto;
        monitoring.MonitoringPriorityId = request.iPrioridadMonitoreo;
        monitoring.MonitoringTypeId = request.iTipoMonitoreo;
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
                cEstado =
                    monitoring.Approved ? "Finalizado" :
                    monitoring.StartDate > DateTime.Now ? "Sin Iniciar" :
                    monitoring.EndDate < DateTime.Now ? "Pendiente Aprobación Acta" :
                    monitoring.Status == '1' ? "En Progreso" : "Cancelado"
            }
        };
    }
}

public class UpdateMonitoringCommand : CreateMonitoringCommand
{
    public int iMovMonitoreo { get; set; }
}