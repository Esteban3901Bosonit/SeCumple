using MediatR;
using SeCumple.Application.Components.Monitorings.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Monitorings.Commads.CreateMonitoring;

public class CreateMonitoringCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateMonitoringCommand, ProcessResult<MonitoringResponse>>
{
    public async Task<ProcessResult<MonitoringResponse>> Handle(CreateMonitoringCommand request,
        CancellationToken cancellationToken)
    {
        var monitoring = new Monitoring
        {
            InterventionId = request.iMovIntervencion,
            MonitoringTypeId = request.iTipoMonitoreo,
            Topic = request.cAsunto,
            StartDate = request.dFechaInicio,
            EndDate = request.dFechaFin,
            Room = request.cLugar,
            MonitoringPriorityId = request.iPrioridadMonitoreo,
            Approved = false,
            Status = '1',
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<Monitoring>().AddAsync(monitoring);
        
        var recordDocument = new RecordDocument
        {
            MonitoringId = monitoring.Id,
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<RecordDocument>().AddAsync(recordDocument);

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

public class CreateMonitoringCommand : IRequest<ProcessResult<MonitoringResponse>>
{
    public int iMovIntervencion { get; set; }
    public int iTipoMonitoreo { get; set; }
    public string cAsunto { get; set; }
    public DateTime dFechaInicio { get; set; }
    public DateTime dFechaFin { get; set; }
    public string cLugar { get; set; }
    public int iPrioridadMonitoreo { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}