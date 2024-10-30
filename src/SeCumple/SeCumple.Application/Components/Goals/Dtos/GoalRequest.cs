namespace SeCumple.Application.Components.Goals.Dtos;

public class GoalRequest
{
    public int iMeta { get; set; }
    public int iEstadoObs { get; set; } = 0;
    public string cObservado { get; set; }
    public string cRespuesta { get; set; }
}