using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MovIndicadorPeriodo")]
public class Period : Base
{
    [Column("iMovIndicadorPeriodo")]
    public int Id { get; set; }
    [Column("iMovIndicador")]
    public int IndicatorId { get; set; }
    [Column("iDetPlanCumpAnio")]
    public int PlanAnioId { get; set; }
    [Column("iTipoMedicion")]
    public int MeasureTypeId { get; set; }
    [Column("iMaePerioricidad")]
    public int PeriodicityId { get; set; }
    [Column("iMetaAnual")]
    public int AnnualGoal { get; set; }
    [Column("iLineaBaseAnual")]
    public int AnnualBaseline { get; set; }
}