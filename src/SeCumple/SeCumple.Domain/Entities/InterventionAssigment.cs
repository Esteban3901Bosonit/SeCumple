using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MovAsignacionIntervencion")]
public class InterventionAssignment : Base
{
    [Column("iMovAsignacionIntervencion")]
    public int Id { get; set; }
    [Column("iMovAsignacion")]
    public int AssigmentId { get; set; }
    [Column("iMovIntervencion")]
    public int InterventionId { get; set; }
    public virtual Assignment? Assigment { get; set; }
}