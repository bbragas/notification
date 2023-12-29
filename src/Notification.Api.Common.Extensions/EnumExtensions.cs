namespace Notification.Api.Common.Extensions;

public static class EnumExtensions
{
    public static bool IsDefined<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Enum.IsDefined(value);
}
