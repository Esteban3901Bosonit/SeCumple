using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MovAsignacion")]
public class Assignment : Base
{
    [Column("iMovAsignacion")]
    public int Id { get; set; }
    [Column("iCodUsuario")]
    public int UserId { get; set; }      
    [Column("iRol")]
    public int RoleId { get; set; }
    [Column("iPersona")]
    public int? PersonId { get; set; }
    [Column("iMaeSector")]
    public int SectorId { get; set; }
    public virtual Sector? Sector { get; set; }
    [Column("iMaeUnidadOrganica")]
    public int? OrganicUnitId { get; set; }
    [Column("iDetPlanCumplimiento")]
    public int PlanId { get; set; } 
    [Column("cUserName")]
    public string? Username { get; set; }
    [Column("cActivo")]
    public string? Active { get; set; }
    public virtual Plan? Plan { get; set; }
}