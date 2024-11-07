using System.Runtime.Serialization;

namespace SeCumple.CrossCutting.Enums;

public enum StatusPlanEnum
{
    [EnumMember(Value = "EN ELABORACION")]
    Pending = 1,
    [EnumMember(Value = "APROBADO")]
    Approved = 2,
    [EnumMember(Value = "CERRADO")]
    Closed=3,
    [EnumMember(Value = "ACTUALIZACION")]
    Update=4
}