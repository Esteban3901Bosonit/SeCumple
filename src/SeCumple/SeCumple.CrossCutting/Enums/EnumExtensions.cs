using System.Reflection;
using System.Runtime.Serialization;

namespace SeCumple.CrossCutting.Enums;


public static class EnumExtensions
{
    public static string GetEnumMemberValue<T>(this T enumValue) where T : Enum
    {
        var memberInfo = typeof(T).GetMember(enumValue.ToString());
        if (memberInfo.Length <= 0) return enumValue.ToString();
        var attributes = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
        if (attributes.Length > 0)
        {
            return ((EnumMemberAttribute)attributes[0]).Value ?? enumValue.ToString();
        }
        return enumValue.ToString();
    }
    
    public static T? GetEnumValueFromEnumMemberValue<T>(string value) where T : struct, Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
            if (attribute != null && attribute.Value == value)
            {
                return (T)field.GetValue(null);
            }
        }
        return null; 
    }
}