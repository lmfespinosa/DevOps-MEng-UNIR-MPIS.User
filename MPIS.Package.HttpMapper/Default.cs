#region "Libraries"

using System;
using System.Globalization;

#endregion


namespace MPIS.Package.HttpMapper
{
    public static class Default
    {
        internal static decimal GetDefaultDecimalOrValue(string parameter) =>
            parameter == string.Empty
                ? 0
                : decimal.Parse(parameter, CultureInfo.InvariantCulture);

        internal static int GetDefaultIntOrValue(string parameter) =>
            parameter == string.Empty
                ? 0
                : int.Parse(parameter);

        internal static long GetDefaultLongOrValue(string parameter) =>
            parameter == string.Empty
                ? 0
                : long.Parse(parameter, CultureInfo.InvariantCulture);

        internal static double GetDefaultDoubleOrValue(string parameter) =>
            parameter == string.Empty
                ? 0
                : double.Parse(parameter, CultureInfo.InvariantCulture);

        internal static bool GetDefaultBooleanOrValue(string parameter) =>
            parameter == string.Empty || Parser.BoolParser(parameter);

        internal static Guid GetDefaultGuidOrValue(string parameter) =>
            parameter == string.Empty
                ? default
                : Guid.Parse(parameter);

        internal static decimal? GetDefaultDecimalNullableOrValue(string parameter) =>
            string.IsNullOrEmpty(parameter)
                ? default(decimal?)
                : decimal.Parse(parameter, CultureInfo.InvariantCulture);

        internal static int? GetDefaultIntNullableOrValue(string parameter) =>
            string.IsNullOrEmpty(parameter)
                ? default(int?)
                : int.Parse(parameter);

        internal static long? GetDefaultLongNullableOrValue(string parameter) =>
            string.IsNullOrEmpty(parameter)
                ? default(long?)
                : long.Parse(parameter, CultureInfo.InvariantCulture);

        internal static double? GetDefaultDoubleNullableOrValue(string parameter) =>
            string.IsNullOrEmpty(parameter)
                ? default(double?)
                : double.Parse(parameter, CultureInfo.InvariantCulture);

        internal static bool? GetDefaultBooleanNullableOrValue(string parameter) =>
            string.IsNullOrEmpty(parameter)
                ? default(bool?)
                : Parser.BoolParser(parameter);

        internal static Guid? GetDefaultGuidNullableOrValue(string parameter) =>
            string.IsNullOrEmpty(parameter)
                ? default(Guid?)
                : Guid.Parse(parameter);
    }
}
