using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("DetUbigeo")]
public class Ubigeo:Base
{    
    [Column("iDetUbigeo")]
    public int Id { get; set; }
    [Column("iMovIntervencion")]
    public int InterventionId { get; set; }
    [Column("iMaeRegion")]
    public int RegionId { get; set; }
    [Column("iMaeProvincia")]
    public int? ProvinceId { get; set; }
    [Column("iMaeDistrito")]
    public int? DistrictId { get; set; }
    [Column("cUbigeo")]
    public string? Code { get; set; }
}