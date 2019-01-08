using System;
using System.Reflection;

namespace EnumUtils
{
    public static class ReflectionExtensions
    {
        public static bool IsEnum(this Type value)
        {
#if NET40 || NET45
            return value.IsEnum;
#else
            return value.GetTypeInfo().IsEnum;
#endif
        }
    }
}
