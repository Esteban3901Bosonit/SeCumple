using MediatR;
using SeCumple.Application.Components.Alerts.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Alerts.Commands.DeleteAlert;

public class DeleteAlertCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAlertCommand, ProcessResult<AlertResponse>>
{
    public async Task<ProcessResult<AlertResponse>> Handle(DeleteAlertCommand request,
        CancellationToken cancellationToken)
    {
        var alert = await unitOfWork.Repository<Alert>().GetByIdAsync(request.iMaeAlerta);

        alert.Status = '0';
        alert.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Alert>().UpdateAsync(alert);

        return new ProcessResult<AlertResponse>
        {
            Data = new AlertResponse
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
            }
        };
    }
}

public class DeleteAlertCommand : IRequest<ProcessResult<AlertResponse>>
{
    public int iMaeAlerta { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}