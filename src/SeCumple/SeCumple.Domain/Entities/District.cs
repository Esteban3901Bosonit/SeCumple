using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeDistrito")]
public class District:Base
{
    [Column("iMaeDistrito")]
    public int Id { get; set; }
    [Column("iMaeProvincia")]
    public int ProvinceId { get; set; }
    [Column("cNombre")]
    public string Name { get; set; }
    [Column("cUbigeo")]
    public string Ubigeo { get; set; }
    public virtual Province? Province{ get; set; }
}