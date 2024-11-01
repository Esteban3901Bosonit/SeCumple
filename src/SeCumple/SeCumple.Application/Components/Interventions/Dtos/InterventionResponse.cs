namespace SeCumple.Application.Components.Interventions.Dtos;

public class InterventionResponse
{
    public int iMovIntervencion { get; set; }
    public string? cNombre { get; set; }
    public int iMaeEje { get; set; }
    public string cNum { get; set; }
    public string cNombreEje { get; set; }
    public int iMaeLineamiento { get; set; }
    public string cNumLineamiento { get; set; }
    public string cNombreLineamiento { get; set; }
    public int iMaeSector { get; set; }
    public string cNombreSector { get; set; }
    public int iMaeUnidadOrganica { get; set; }
    public string cNombreUnidadOrganica { get; set; }
    public string cEstado { get; set; }
    public string cEstadoIntervencion { get; set; }
    public string? cCUI { get; set; }
    public string? cCodigoPCG { get; set; }
    public int? iTipoIntervencion { get; set; }
    public int? iSubTipoIntervencion { get; set; }
    public int[]? iFuente { get; set; }
    public int? iPrioridad { get; set; }
    public RegionInterventionResponse[]? Ubigeos { get; set; }
}