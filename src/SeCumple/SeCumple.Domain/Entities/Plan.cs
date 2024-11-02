using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Constants;
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
    public string? CurrentTitle { get; set; } = PlanMessages.CurrentStatusTitle;
    [Column("cTituloDescripAcciones")]
    public string? ActionsDescription { get; set; } = PlanMessages.ActionsDescriptionTitle;
    [Column("cTituloDescripAlertas")]
    public string? AlertsDescription { get; set; } = PlanMessages.AlertsDescripcionTitle;
    [Column("iVersion")]
    public int? Version { get; set; }
    [Column("iDetPlanPadre")]
    public int? ParentPlanId { get; set; }
    [Column("cCodDocumentoAprobacion")]
    public string? ApprovalDocumentCode { get; set; }
    [Column("iMaeArchivoAprobacion")]
    public int? ApprovalFileId { get; set; }
    public virtual Document? Document { get; set; }
    public virtual FileUploaded? ApprovalFile { get; set; }
    public virtual ICollection<Assignment>? Assigments { get; set; }
    public virtual ICollection<PlanAnio>? PlanAnios { get; set; }
    public virtual ICollection<SectorsPlan>? Sectors { get; set; }

}