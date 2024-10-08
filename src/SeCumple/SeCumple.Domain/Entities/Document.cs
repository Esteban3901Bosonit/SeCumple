using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeDispositivo")]
public class Document: Base
{
    [Column("iMaeDispositivo")]
    public int Id { get; set; }
    [Column("cNumDispositivo")]
    public string? DocumentCode { get; set; }
    [Column("dFechaDispositivo")]
    public DateTime? DocumentDate { get; set; }
    [Column("iTipoDispositivo")]
    public int DocumentType { get; set; }
    public string? DocumentDateFormatted => DocumentDate!.Value.ToString("yyyy/MM/dd");
    [Column("cLink")]
    public string? Url { get; set; } 
    [Column("cEstVigencia")]
    public char Active { get; set; }
    public string? EsActivo => Active == '1' ? "SI" : "NO";
    [Column("iValidado")]
    public int Validated { get; set; }
}