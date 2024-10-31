using MediatR;
using SeCumple.Application.Components.Alerts.Commands.CreateAlert;
using SeCumple.Application.Components.Alerts.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Alerts.Commands.UpdateAlert;

public class UpdateAlertCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAlertCommand, ProcessResult<AlertResponse>>
{
    public async Task<ProcessResult<AlertResponse>> Handle(UpdateAlertCommand request,
        CancellationToken cancellationToken)
    {
        var alert = await unitOfWork.Repository<Alert>().GetByIdAsync(request.iMaeAlerta);

        alert.AlertTypeId = request.iTipoAlerta;
        alert.Name = request.cNombreAlerta;
        alert.PlanIds = request.cPlanes;
        alert.Stages = request.cEtapas;
        alert.SectorIds = request.cSectoresDestinatarios;
        alert.RoleIds = request.cRolesDestinatarios;
        alert.OtherEmails = request.cOtrosDestinatarios;
        alert.Message = request.cMensaje;
        alert.StartDate = request.dFechaInicio;
        alert.EndDate = request.dFechaFin;
        alert.PeriodicityId = request.iPerioricidad;
        alert.NotificationDates = request.cFechasNotificacion;
        alert.DaysBeforeExpiration = request.cDiasPreviosVencimiento;
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

public class UpdateAlertCommand : CreateAlertCommand
{
    public int iMaeAlerta { get; set; }
}