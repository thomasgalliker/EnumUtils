using System;

namespace EnumUtils
{
    internal static class ReflectionExtensions
    {
        public static bool IsEnum(this Type value)
        {
            return value.IsEnum;
        }
    }
}
