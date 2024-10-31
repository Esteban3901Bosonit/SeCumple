using MediatR;
using SeCumple.Application.Components.Alerts.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Alerts.Commands.CreateAlert;

public class CreateAlertCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAlertCommand, ProcessResult<AlertResponse>>
{
    public async Task<ProcessResult<AlertResponse>> Handle(CreateAlertCommand request,
        CancellationToken cancellationToken)
    {
        var alert = new Alert
        {
            AlertTypeId = request.iTipoAlerta,
            Name = request.cNombreAlerta,
            PlanIds = request.cPlanes,
            Stages = request.cEtapas,
            SectorIds = request.cSectoresDestinatarios,
            RoleIds = request.cRolesDestinatarios,
            OtherEmails = request.cOtrosDestinatarios,
            Message = request.cMensaje,
            StartDate = request.dFechaInicio,
            EndDate = request.dFechaFin,
            PeriodicityId = request.iPerioricidad,
            NotificationDates = request.cFechasNotificacion,
            DaysBeforeExpiration = request.cDiasPreviosVencimiento,
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<Alert>().AddAsync(alert);

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

public class CreateAlertCommand : IRequest<ProcessResult<AlertResponse>>
{
    public int iTipoAlerta { get; set; }
    public string cNombreAlerta { get; set; }
    public string? cPlanes { get; set; }
    public string? cEtapas { get; set; }
    public string? cSectoresDestinatarios { get; set; }
    public string? cRolesDestinatarios { get; set; }
    public string? cOtrosDestinatarios { get; set; }
    public string cMensaje { get; set; }
    public DateTime dFechaInicio { get; set; }
    public DateTime dFechaFin { get; set; }
    public int iPerioricidad { get; set; }
    public string? cFechasNotificacion { get; set; }
    public string? cDiasPreviosVencimiento { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}