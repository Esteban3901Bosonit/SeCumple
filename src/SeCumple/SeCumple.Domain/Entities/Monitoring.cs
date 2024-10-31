using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MovMonitoreo")]
public class Monitoring:Base
{
    [Column("iMovMonitoreo")]
    public int Id { get; set; }
    [Column("iMovIntervencion")]
    public int InterventionId { get; set; }
    [Column("iTipoMonitoreo")]
    public int MonitoringTypeId { get; set; }
    [Column("cAsunto")]
    public string Topic { get; set; }
    [Column("dFechaInicio")]
    public DateTime StartDate { get; set; }
    [Column("dFechaFin")]
    public DateTime EndDate { get; set; }
    [Column("cLugar")]
    public string Room { get; set; }
    [Column("iPrioridadMonitoreo")]
    public int MonitoringPriorityId { get; set; }
    [Column("iAprobado")]
    public bool Approved { get; set; }
    [Column("iMaeArchivoEvidencia")]
    public int? EvidenceFileId { get; set; }
    public virtual RecordDocument? RecordDocument { get; set; }
}