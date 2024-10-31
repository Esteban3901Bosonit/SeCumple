using MediatR;
using SeCumple.Application.Components.Monitorings.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Monitorings;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Monitorings.Queries.ListMonitoring;

public class ListMonitoringQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListMonitoringQuery, ProcessResult<PaginationResponse<MonitoringResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<MonitoringResponse>>> Handle(ListMonitoringQuery request,
        CancellationToken cancellationToken)
    {
        var monitoringSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var monitoringSpec = new MonitoringSpecification(monitoringSpecParams);
        var monitorings = await unitOfWork.Repository<Monitoring>().GetAllWithSpec(monitoringSpec);

        var spectCount = new MonitoringForCountingSpecification(monitoringSpecParams);
        var totalMonitoring = await unitOfWork.Repository<Monitoring>().CountAsync(spectCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalMonitoring) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var monitoringResponse = monitorings.Select(monitoring => new MonitoringResponse
            {
                iMovMonitoreo = monitoring.Id,
                iMovIntervencion = monitoring.InterventionId,
                iTipoMonitoreo = monitoring.MonitoringTypeId,
                cAsunto = monitoring.Topic,
                dFechaInicio = monitoring.StartDate,
                dFechaFin = monitoring.EndDate,
                cLugar = monitoring.Room,
                iPrioridadMonitoreo = monitoring.MonitoringPriorityId,
                iDetActa = monitoring.RecordDocument!.Id,
                cEstado =
                    monitoring.Approved ? "Finalizado" :
                    monitoring.StartDate > DateTime.Now ? "Sin Iniciar" :
                    monitoring.EndDate < DateTime.Now ? "Pendiente Aprobación Acta" :
                    "En Progreso"
            }
        );

        return new ProcessResult<PaginationResponse<MonitoringResponse>>
        {
            Data = new PaginationResponse<MonitoringResponse>
            {
                Count = totalMonitoring,
                Data = monitoringResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = monitorings.Count
            }
        };
    }
}

public class ListMonitoringQuery : PaginationRequest,
    IRequest<ProcessResult<PaginationResponse<MonitoringResponse>>>
{
}