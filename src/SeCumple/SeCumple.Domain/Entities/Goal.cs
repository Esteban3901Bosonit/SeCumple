using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MovMeta")]
public class Goal: Base
{
    [Column("iMovMeta")]
    public int Id { get; set; }
    [Column("iMovIndicadorPeriodo")]
    public int PeriodId { get; set; }
    public virtual Period? Period { get; set; }
    [Column("iInformarMeta")]
    public decimal GoalValue { get; set; }
    [Column("iFlag")]
    public int? Flag { get; set; }
    [Column("iMeta")]
    public decimal? FinalGoal { get; set; }
    [Column("iOrden")]
    public int Order { get; set; }
    [Column("cUrlArchivo")]
    public string? Url { get; set; }
    [Column("cComentario")]
    public string? Comment { get; set; }
    [Column("cNombreArchivo")]
    public string? Filename { get; set; }
    [Column("iEstadoMeta")]
    public int GoalStatusId { get; set; }
    [Column("iEstadoRealizado")]
    public int DoneStatusId { get; set; }
    [Column("iEstadoObservado")]
    public int CheckedStatusId { get; set; }
    [Column("cObservado")]
    public string? Annotation { get; set; }
    [Column("cRespuesta")]
    public string? Answer { get; set; }
    [Column("cEstadoActual")]
    public string? CurrentStatus { get; set; }
    [Column("cDescripAcciones")]
    public string? ActionDescriptions { get; set; }
    [Column("cDescripAlertas")]
    public string? AlertDescriptions { get; set; }
    [Column("iEstadoCumplido")]
    public int? CompliancetatusId { get; set; }
    [Column("iCumpleAutomatico")]
    public int? AutomatedCompliance { get; set; }
}