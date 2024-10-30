namespace SeCumple.Application.Components.Goals.Dtos;

public class PeriodResponse
{
    public int iMovIndicadorPeriodo { get; set; }
    public int iMovIndicador { get; set; }
    public int iDetPlanCumpAnio { get; set; }
    public int iTipoMedicion { get; set; }
    public int iMaePeriodicidad { get; set; }
    public int iMetaAnual { get; set; }
    public int iLineaBaseAnual { get; set; }
    public char cEstado { get; set; }
    public List<GoalResponse> ListaMetas { get; set; }
}