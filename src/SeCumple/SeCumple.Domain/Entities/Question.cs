using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeEje")]
public class Question : Base
{
    [Column("iMovPregunta")]
    public int Id { get; set; }
    [Column("iNum")]
    public int? Numeral { get; set; }
    [Column("cPregunta")]
    public string? Statement { get; set; }
    [Column("cRespuesta")]
    public string? Answer { get; set; }
    [Column("iMovSeguimiento")]
    public int FollowUpId { get; set; }
    [Column("iTipoPregunta")]
    public int QuestionTypeId { get; set; }
}