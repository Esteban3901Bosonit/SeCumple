using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeSector")]
public class Sector : Base
{
    [Column("iMaeSector")]
    public int Id { get; set; }
    [Column("cNombre")]
    public string? Name { get; set; }
    [Column("cSigla")]
    public string? Acronym { get; set; }
    [Column("cDescripcionCorta")]
    public string? Description { get; set; }
}