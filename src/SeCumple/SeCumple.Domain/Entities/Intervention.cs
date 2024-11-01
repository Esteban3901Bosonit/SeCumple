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
    [Column("iMaeSector")]
    public string? SectorIds { get; set; }
    [Column("iFuente")]
    public string? SourceIds { get; set; }
    [Column("cTipoRegion")]
    public string? RegionType { get; set; }
    [Column("iMaeRegion")]
    public string? RegionIds { get; set; }
    [Column("iMaeProvincia")]
    public string? ProvinceIds { get; set; }
    [Column("iMaeDistrito")]
    public string? DistrictIds { get; set; }
    [Column("iTipoIntervencion")]
    public int? InterventionTypeId { get; set; }
    [Column("cOtroTipoIntervencion")]
    public string? OtherInterventionType { get; set; }
    [Column("iSubTipoIntervencion")]
    public int? SubInterventionTypeId { get; set; }
    [Column("iPrioridad")]
    public int? PriorityId { get; set; }
    [Column("cCodigoUbigeo")]
    public string? UbigeoCode { get; set; }
    [Column("cCodigoPCG")]
    public string? PCGCode { get; set; }
    [Column("cCUI")]
    public string? CUI { get; set; }
    public virtual ICollection<InterventionAssignment>? AssignedUsers { get; set; }
    public virtual ICollection<Ubigeo>? Regions { get; set; }
}