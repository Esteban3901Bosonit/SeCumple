using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("DetPlanCumplimientoAnio")]
public class PlanAnio : Base
{
    [Column("iDetPlanCumpAnio")]
    public int Id { get; set; }
    [Column("iDetPlanCumplimiento")]
    public int PlanId { get; set; }
    public virtual Plan? Plan { get; set; }
    [Column("iAnio")]
    public int Anio { get; set; }
}