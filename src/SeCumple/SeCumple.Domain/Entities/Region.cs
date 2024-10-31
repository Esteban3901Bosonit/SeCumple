using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeRegion")]

public class Region:Base
{
    [Column("iMaeRegion")]
    public int Id { get; set; }
    [Column("cNombre")]
    public string Name { get; set; }
    [Column("cUbigeo")]
    public string Ubigeo { get; set; }
    public ICollection<Province>? Provinces { get; set; }
}