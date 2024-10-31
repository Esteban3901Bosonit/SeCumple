using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeAlerta")]
public class Alert : Base
{
    [Column("iMaeAlerta")]
    public int Id { get; set; }
    [Column("iTipoAlerta")]
    public int AlertTypeId { get; set; }
    [Column("cNombreAlerta")]
    public string Name { get; set; }
    [Column("cPlanes")]
    public string? PlanIds { get; set; }
    [Column("cEtapas")]
    public string? Stages { get; set; }
    [Column("cModulo")]
    public string? Module { get; set; }
    [Column("cSectoresDestinatarios")]
    public string? SectorIds { get; set; }
    [Column("cRolesDestinatarios")]
    public string? RoleIds { get; set; }
    [Column("cOtrosDestinatarios")]
    public string? OtherEmails { get; set; }
    [Column("cMensaje")]
    public string Message { get; set; }
    [Column("dFechaInicio")]
    public DateTime StartDate { get; set; }
    [Column("dFechaFin")]
    public DateTime EndDate { get; set; }
    [Column("iPerioricidad")]
    public int PeriodicityId { get; set; }
    [Column("cFechasNotificacion")]
    public string? NotificationDates { get; set; }
    [Column("cDiasPreviosVencimiento")]
    public string? DaysBeforeExpiration { get; set; }
    
}