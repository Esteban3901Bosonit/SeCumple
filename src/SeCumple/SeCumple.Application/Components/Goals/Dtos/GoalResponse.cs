namespace SeCumple.Application.Components.Goals.Dtos;

public class GoalResponse
{
    public int iMovMeta { get; set; }
    public int iMovIndicadorPeriodo { get; set; }
    public int? iInformarMeta { get; set; }
    public int? iFlag { get; set; }
    public int iMeta { get; set; }
    public int iOrden { get; set; }
    public char cEstado { get; set; }
    public int? iEstadoMeta { get; set; }
    public string? cEstadoMeta { get; set; }
    public int? iEstadoRealizado { get; set; }
    public int? iEstadoObs { get; set; }
    public string? cObservado { get; set; }
    public string? cRespuesta { get; set; }
    public string? cComentario { get; set; }
}