using System.Text.RegularExpressions;

namespace TreeGecko.Library.Common.Helpers
{
    public class GuidHelper
    {
        private const string GUID_CHECK =
            "[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}|\\([A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}\\)|\\{[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}\\}";

        public static bool IsValidGuidString(string _guidString)
        {
            if (_guidString != null)
            {
                return Regex.IsMatch(_guidString, GUID_CHECK, RegexOptions.IgnoreCase);
            }

            return false;
        }
    }
}