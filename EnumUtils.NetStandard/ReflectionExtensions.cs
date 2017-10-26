using System;
using System.Reflection;

namespace EnumUtils
{
    internal static class ReflectionExtensions
    {
        public static bool IsEnum(this Type value)
        {
            return value.GetTypeInfo().IsEnum;
        }
    }
}
