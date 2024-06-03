using System.ComponentModel;

namespace TestWebApi.Helpers;

public static class EnumExtensions
{
    public static string ToDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes is { Length: > 0 } ? ((DescriptionAttribute)attributes[0]).Description : value.ToString();
    }
}