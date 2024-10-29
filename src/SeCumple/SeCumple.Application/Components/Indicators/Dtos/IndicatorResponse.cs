namespace SeCumple.Application.Components.Indicators.Dtos;

public class IndicatorResponse
{
    public int iMovIndicador { get; set; }
    public string? cNombre { get; set; }
    public string? cDescripcion { get; set; }
    public string? cDescripcionIndicador { get; set; }
    public int iUnidadMedida { get; set; }
    public string cNombreUnidadMedida { get; set; }
    public int iMovIntervencion { get; set; }
    public string? cNombreIntervencion { get; set; }
    public int iTipoIndicador { get; set; }
    public string cNombreTipoIndicador { get; set; }
    public string cAccion { get; set; }
    public int iMovIndicadorPadre { get; set; }
    public int iEstadoIndicador { get; set; }
    public string cEstadoIndicador { get; set; }
    public int Nivel { get; set; }
    public int? iNivelTipoIndicador { get; set; }
    public int iMaeEje { get; set; }
    public string? cTitulo { get; set; }
    public int iDetPlanCumplimiento { get; set; }
    public string? cNombrePlan { get; set; }
    public int iMaeLineamiento { get; set; }
    public string? cNombreLineamiento { get; set; }
    public int iMaeSector { get; set; }
    public int iMaeUnidadOrganica { get; set; }
    public int iDetPlanCumpAnio { get; set; }
    public int iAnio { get; set; }
    public decimal? iLineaBaseAnual { get; set; }
    public decimal? iMetaAnual { get; set; }
    public int iMovIndicadorPeriodo { get; set; }
    // public List<MovMetaEntity>? ListaMetas { get; set; }
}