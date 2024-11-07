using System.Runtime.Serialization;

namespace SeCumple.CrossCutting.Enums;

public enum StatusInterventionEnum
{
    [EnumMember(Value = "PENDIENTE")]
    Pending=1,
    [EnumMember(Value = "ACTIVO")]
    Active=2,
    [EnumMember(Value = "INACTIVO")]
    Inactive=3
}
