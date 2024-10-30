using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MovIntervencion")]
public class Intervention : Base
{
    [Column("iMovIntervencion")]
    public int Id { get; set; }
    [Column("cNombre")]
    public string? Name { get; set; }      
    [Column("iMaeUnidadOrganica")]
    public int OrganicUnitId { get; set; }
    public virtual OrganicUnit? OrganicUnit { get; set; }
    [Column("iMaeLineamiento")]
    public int GuidelineId { get; set; }
    public virtual GuideLine? GuideLine { get; set; }

    [Column("iDetPlanCumplimiento")]
    public int PlanId { get; set; }
    public virtual Plan? Plan { get; set; }
    [Column("iEstadoAsignacion")]
    public int AssignmentStatusId { get; set; }
    [Column("iEstadoIntervencion")]
    public int InterventionStatusId { get; set; }
    public virtual ICollection<InterventionAssignment>? AssignedUsers { get; set; }
}