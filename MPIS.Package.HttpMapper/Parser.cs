

namespace MPIS.Package.HttpMapper
{
    public static class Parser
    {
        internal static bool BoolParser(string parameter)
        {
            parameter = parameter.Trim().ToLower();

            if (parameter == "1" || parameter == "t")
                return true;

            if (parameter == "0")
                return false;

            return true;
        }
    }
}
