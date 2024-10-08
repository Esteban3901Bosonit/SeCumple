using SeCumple.CrossCutting.Attributes;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MaeDispositivo")]
public class Device: Base
{
    [Column("iMaeDispositivo")]
    public int Id { get; set; }
    [Column("cNumDispositivo")]
    public string? DeviceCode { get; set; }
    [Column("dFechaDispositivo")]
    public DateTime? DeviceDate { get; set; }
    public string? DeviceDateFormatted => DeviceDate!.Value.ToString("yyyy/MM/dd");
    [Column("cLink")]
    public string? Url { get; set; } 
    [Column("cEstVigencia")]
    public char Active { get; set; }
    public string? EsActivo => Active == '1' ? "SI" : "NO";
    [Column("iValidado")]
    public bool Validated { get; set; }
}