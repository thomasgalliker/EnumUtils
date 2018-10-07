namespace EnumUtils
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if the given type <typeparamref name="T"/> is an enum type.
        /// </summary>
        /// <typeparam name="T">Generic param type.</typeparam>
        /// <returns>True if given type is an enum, otherwise, false.</returns>
        public static bool IsEnum<T>(this T value)
        {
            return EnumHelper.IsEnum<T>();
        }

        /// <summary>
        /// Checks if the given parameter <paramref name="value"/> is an enum type.
        /// </summary>
        /// <returns>True if given type is an enum, otherwise, false.</returns>
        /// <param name="value"></param>
        /// <exception cref="T:System.ArgumentNullException">If <paramref name="value"/> is null.</exception>
        public static bool IsEnum(this object value)
        {
            return EnumHelper.IsEnum(value);
        }
    }
}
