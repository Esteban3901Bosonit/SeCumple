using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("DetSectoresPlan")]
public class SectorsPlan: Base
{
    
    [Column("iDetSectoresPlan")]
    public int Id { get; set; }
    [Column("iDetPlanCumplimiento")]
    public int PlanId { get; set; }
    public virtual Plan Plan { get; set; }
    [Column("iMaeSector")]
    public int SectorId { get; set; }
}