namespace SeCumple.Application.Components.Monitorings.Dtos;

public class MonitoringResponse
{
    public int iMovMonitoreo { get; set; }
    public int iMovIntervencion { get; set; }
    public int iTipoMonitoreo { get; set; }
    public string cAsunto { get; set; }
    public DateTime dFechaInicio { get; set; }
    public DateTime dFechaFin { get; set; }
    public string cLugar { get; set; }
    public int iPrioridadMonitoreo { get; set; }
    public bool iAprobado { get; set; }
    public int iDetActa { get; set; }
    public string cEstado { get; set; }
    public int? iMaeArchivoEvidencia { get; set; }
}