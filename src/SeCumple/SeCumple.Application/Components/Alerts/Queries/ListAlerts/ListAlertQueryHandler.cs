using MediatR;
using SeCumple.Application.Components.Alerts.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Alerts;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Alerts.Queries.ListAlerts;

public class ListAlertQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListAlertQuery, ProcessResult<PaginationResponse<AlertResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<AlertResponse>>> Handle(ListAlertQuery request,
        CancellationToken cancellationToken)
    {
        var alertSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var alertSpec = new AlertSpecification(alertSpecParams);
        var alerts = await unitOfWork.Repository<Alert>().GetAllWithSpec(alertSpec);

        var spectCount = new AlertForCountingSpecification(alertSpecParams);
        var totalAlerts = await unitOfWork.Repository<Alert>().CountAsync(spectCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalAlerts) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var alertResponse = alerts.Select(alert => new AlertResponse
        {
            iMaeAlerta = alert.Id,
            iTipoAlerta = alert.AlertTypeId,
            cNombreAlerta = alert.Name,
            cPlanes = alert.PlanIds,
            cEtapas = alert.Stages,
            cSectoresDestinatarios = alert.SectorIds,
            cRolesDestinatarios = alert.RoleIds,
            cOtrosDestinatarios = alert.OtherEmails,
            cMensaje = alert.Message,
            dFechaInicio = alert.StartDate,
            dFechaFin = alert.EndDate,
            iPerioricidad = alert.PeriodicityId,
            cFechasNotificacion = alert.NotificationDates,
            cDiasPreviosVencimiento = alert.DaysBeforeExpiration
        });

        return new ProcessResult<PaginationResponse<AlertResponse>>
        {
            Data = new PaginationResponse<AlertResponse>
            {
                Count = totalAlerts,
                Data = alertResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = alerts.Count
            }
        };
    }
}

public class ListAlertQuery : PaginationRequest,
    IRequest<ProcessResult<PaginationResponse<AlertResponse>>>
{
}