using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("DetActa")]
public class RecordDocumentElement:Base
{
    [Column("iDetElementoActa")]
    public int Id { get; set; }
    [Column("iDetActa")]
    public int RecordDocumentId { get; set; }
    [Column("iNumeral")]
    public int Numeral { get; set; }
    [Column("cTema")]
    public string Content { get; set; }
    [Column("iTipoElementoActa")]
    public int RecordDocumentElementTypeId { get; set; }
}