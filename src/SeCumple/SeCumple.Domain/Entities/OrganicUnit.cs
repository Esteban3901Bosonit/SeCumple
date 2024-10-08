using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeUnidadOrganica")]
public class OrganicUnit : Base
{
    [Column("iMaeUnidadOrganica")]
    public int Id { get; set; }
    [Column("cNombre")]
    public string? Name { get; set; }
    [Column("cSigla")]
    public string? Acronym { get; set; }
    [Column("iMaeSector")]
    public int SectorId { get; set; }
}