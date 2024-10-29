using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeEje")]
public class Axis : Base
{
    [Column("iMaeEje")]
    public int Id { get; set; }
    [Column("iMaeDispositivo")]
    public int DocumentId { get; set; }
    [Column("cNum")]
    public string? Numeral { get; set; }
    [Column("cTitulo")]
    public string? Title { get; set; }
    [Column("cDescripcion")]
    public string? Description { get; set; }
    [Column("cValidado")]
    public char Validated { get; set; }
    public virtual ICollection<GuideLine>? GuideLines { get; set; }
}