using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumUtils
{
    public static class EnumHelper
    {
        private static readonly Random Rng = new Random();

        /// <summary>
        /// Checks if the given type <typeparamref name="T"/> is an enum type.
        /// </summary>
        /// <typeparam name="T">Generic param type.</typeparam>
        /// <returns>True if given type is an enum, otherwise, false.</returns>
        public static bool IsEnum<T>()
        {
            return GetUnderlyingType<T>().IsEnum();
        }

        /// <summary>
        /// Checks if the given parameter <paramref name="value"/> is an enum type.
        /// </summary>
        /// <returns>True if given type is an enum, otherwise, false.</returns>
        /// <param name="value"></param>
        /// <exception cref="T:System.ArgumentNullException">If <paramref name="value"/> is null.</exception>
        public static bool IsEnum(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return GetUnderlyingType(value.GetType()).IsEnum();
        }

        public static IEnumerable<TEnum> GetValues<TEnum>()
        {
            var enumType = GetUnderlyingType(typeof(TEnum));

            return GetValues(enumType).Cast<TEnum>().ToList();
        }

        public static Array GetValues(Type enumType)
        {
            ThrowIfNotEnum(enumType);

            return Enum.GetValues(enumType);
        }

        public static object GetValue(Type enumType, int index)
        {
            return Enumerable.ToList(GetValues(enumType).Cast<object>())[index];
        }

        public static string GetName<TEnum>(TEnum value)
        {
            var enumType = GetUnderlyingType(typeof(TEnum));
            ThrowIfNotEnum(enumType);

            return Enum.GetName(enumType, value);
        }

        public static IEnumerable<string> GetNames<TEnum>()
        {
            var enumType = GetUnderlyingType(typeof(TEnum));
            ThrowIfNotEnum(enumType);

            return Enum.GetNames(enumType);
        }

        public static TEnum Parse<TEnum>(string value)
        {
            var enumType = GetUnderlyingType(typeof(TEnum));
            ThrowIfNotEnum(enumType);

            return (TEnum) Enum.Parse(enumType, value, true);
        }

        public static TEnum TryParse<TEnum>(string value, bool ignoreCase = true) where TEnum : struct
        {
            var enumType = GetUnderlyingType(typeof(TEnum));
            ThrowIfNotEnum(enumType);

            TEnum returnValue;
            Enum.TryParse(value, ignoreCase, out returnValue);

            return returnValue;
        }

        /// <summary>
        /// Safely cast an integer to the underlying enum value.
        /// </summary>
        /// <typeparam name="TEnum">Generic param type.</typeparam>
        /// <param name="value">The integer value for the desired enum.</param>
        /// <param name="defaultValue">The value returned if the integer does not map to the enum.</param>
        /// <returns>The enum value which maps to the given integer value.</returns>
        public static TEnum Cast<TEnum>(int value, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            var enumType = GetUnderlyingType(typeof(TEnum));
            ThrowIfNotEnum(enumType);

            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)Enum.ToObject(typeof(TEnum), value);
            }

            return defaultValue;
        }

        public static TEnum GetRandom<TEnum>()
        {
            var values = Enum.GetValues(typeof(TEnum));
            var item = Rng.Next(0, values.Length);
            return (TEnum) values.GetValue(item);
        }

        public static Type GetUnderlyingType(Type nullableType)
        {
            var underlyingType = Nullable.GetUnderlyingType(nullableType);
            if (underlyingType != null)
            {
                return underlyingType;
            }

            return nullableType;
        }

        public static Type GetUnderlyingType<T>()
        {
            return GetUnderlyingType(typeof(T));
        }

        private static void ThrowIfNotEnum(Type type)
        {
            if (!type.IsEnum())
            {
                throw new ArgumentException($"Type {type.Name} must be an enum.");
            }
        }
    }
}