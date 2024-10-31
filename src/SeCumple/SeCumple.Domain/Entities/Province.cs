using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeProvincia")]
public class Province:Base
{
    [Column("iMaeProvincia")]
    public int Id { get; set; }
    [Column("iMaeRegion")]
    public int RegionId { get; set; }
    [Column("cNombre")]
    public string Name { get; set; }
    [Column("cUbigeo")]
    public string Ubigeo { get; set; }
    public virtual Region? Region{ get; set; }
    public ICollection<District>? Districts { get; set; }

}