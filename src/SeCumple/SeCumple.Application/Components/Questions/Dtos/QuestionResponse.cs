namespace SeCumple.Application.Components.Questions.Dtos;

public class QuestionResponse
{
    public int? iMovSeguimiento { get; set; }
    public DateTime? dFecha { get; set; }
    public int? iMovMeta { get; set; }
    public decimal? iInformarMeta { get; set; }
    public int? iTipoSeguimiento { get; set; }
    public int? iEstadoCumplido { get; set; }
    public int? iEstadoSeguimiento { get; set; }
    public string? cEstadoSeguimiento { get; set; }
    public string? cTipoSeguimiento { get; set; }
    public string? cComentario { get; set; }
    public string? cObservacionSeguimiento { get; set; }
    public decimal? iMetaParcial { get; set; }
    public string? cUrlArchivo { get; set; }
    public string? cNombreArchivo { get; set; }
    public string? cEstadoActual { get; set; }
    public string? cDescripAcciones { get; set; }
    public string? cDescripAlertas { get; set; }


    public int? iEstadoObs { get; set; }
    public string? cObservado { get; set; }


    public string? cNombreIntervencion { get; set; }
    public string? cTipoIndicador { get; set; }
    public string? cNombreIndicador { get; set; }
    public string? cEmailEspecialistaOCG { get; set; }
    // Alerno sectorial
    public string? cEmailEspecialistaSector { get; set; }

    public string? cLinkLogin { get; set; }
    // public List<MovPreguntaEntity>? ListPregunta { get; set; }
}