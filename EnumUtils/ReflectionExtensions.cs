using System;
using System.Reflection;

namespace EnumUtils
{
    public static class ReflectionExtensions
    {
        public static bool IsEnum(this Type value)
        {
#if NETFRAMEWORK
            return value.IsEnum;
#else
            return value.GetTypeInfo().IsEnum;
#endif
        }
    }
}
