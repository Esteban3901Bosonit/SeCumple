using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeParameter")]
public class Parameter: Base
{
    [Column("iDetParameter")]
    public int Id { get; set; }
    [Column("cNombre")]
    public string? Name { get; set; }
    [Column("cComentarios")]
    public string? Description { get; set; }
}