#region "Libraries"

using System;
using System.Linq;

#endregion

namespace MPIS.Package.HttpMapper
{
    public static class Converter
    {
        public static string SnakeToCamel(string snake) =>
            snake.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                .Aggregate(string.Empty, (s1, s2) => s1 + s2);
    }
}
