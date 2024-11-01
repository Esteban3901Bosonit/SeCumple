namespace SeCumple.Application.Components.Plans.Dtos;

public class PlanResponse
{
    public int? iDetPlanCumplimiento { get; set; }
    public string cNombre { get; set; }
    public int iMaeDispositivo { get; set; }
    public string cNombreDispositivo { get; set; }
    public string cNombreEstado { get; set; }
    public string cEstado { get; set; }
    public string cObservacion { get; set; }
    public DateTime dFechaInicio { get; set; }
    public DateTime dFechaFin { get; set; }
    public int[]? Sectors { get; set; }
    public string cTituloEstadoActual { get; set; }
    public string cTituloDescripAcciones { get; set; }
    public string cTituloDescripAlertas { get; set; }
}