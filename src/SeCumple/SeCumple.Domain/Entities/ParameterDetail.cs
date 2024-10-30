using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("DetParameter")]
public class ParameterDetail : Base
{
    [Column("iDetParameter")]
    public int Id { get; set; }
    [Column("iMaeParameter")]
    public int? ParentId { get; set; }
    [Column("cNombre")]
    public string? Name { get; set; }
    [Column("iValorNumerico")]
    public int? NumericalValue { get; set; }
    [Column("cValorTexto")]
    public string? TextValue { get; set; }
}