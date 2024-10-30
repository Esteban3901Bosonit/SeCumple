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
    [Column("iMaePeriodicidad")]
    public int PeriodicityId { get; set; }
    [Column("iMetaAnual")]
    public decimal AnnualGoal { get; set; }
    [Column("iLineaBaseAnual")]
    public decimal AnnualBaseline { get; set; }
    public virtual ICollection<Goal>? Goals { get; set; }
}