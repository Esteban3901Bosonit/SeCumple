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
    [Column("iMovIntervecion")]
    public int InterventionId { get; set; }
}