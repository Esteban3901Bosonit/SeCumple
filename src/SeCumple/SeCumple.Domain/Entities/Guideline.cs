using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeLineamiento")]
public class GuideLine : Base
{
    [Column("iMaeLineamiento")]
    public int Id { get; set; }
    [Column("iMaeEje")]
    public int AxisId { get; set; }
    [Column("cNum")]
    public string? Numeral { get; set; }
    [Column("cDescripcion")]
    public string? Description { get; set; }
    [Column("cValidado")]
    public string? Validated { get; set; }
}