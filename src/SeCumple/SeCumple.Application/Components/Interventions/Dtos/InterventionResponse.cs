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
    public int IMaeSector { get; set; }
    public string cNombreSector { get; set; }
    public int iMaeUnidadOrganica { get; set; }
    public string cNombreUnidadOrganica { get; set; }
    public string cEstado { get; set; }
}