namespace SeCumple.Application.Components.Alerts.Dtos;

public class AlertResponse
{
    public int iMaeAlerta { get; set; }
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
}