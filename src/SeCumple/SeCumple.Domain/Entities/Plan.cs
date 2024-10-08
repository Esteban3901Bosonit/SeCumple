using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("DetPlanCumplimiento")]
public class Plan : Base
{
    [Column("iDetPlanCumplimiento")]
    public int Id { get; set; }
    [Column("iMaeDispositivo")]
    public int DocumentId { get; set; }      
    [Column("cObservacion")]
    public string? Annotation { get; set; }
    [Column("dFechaInicio")]
    public DateTime StartDate { get; set; }
    [Column("dFechaFin")]
    public DateTime EndDate { get; set; }
    [Column("iTipoDispositivo")]
    public int DocumentTypeId { get; set; }
    [Column("iEstadoPlan")]
    public int PlanStatusId { get; set; }
    [Column("cNombre")]
    public string? Name { get; set; }
    [Column("cTituloEstadoActual")]
    public string? CurrentTitle { get; set; }
    [Column("cTituloDescripAcciones")]
    public string? ActionDescriptions { get; set; }
    [Column("cTituloDescripAlertas")]
    public string? AlertDescriptions { get; set; }

}