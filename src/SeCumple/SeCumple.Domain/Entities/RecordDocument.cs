using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("DetActa")]
public class RecordDocument:Base
{
    [Column("iDetActa")]
    public int Id { get; set; }
    [Column("iMovMonitoreo")]
    public int MonitoringId { get; set; }
    public virtual Monitoring? Monitoring { get; set; }
    [Column("cUbicacionActa")]
    public string FileLocation { get; set; }
    [Column("cFirmaDigitalActa")]
    public string RecordDocumentSign { get; set; }
    public virtual ICollection<Person>? Participants { get; set; }
}