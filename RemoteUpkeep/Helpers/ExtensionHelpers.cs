using System.Text.RegularExpressions;
using System.Web;

namespace RemoteUpkeep.Helpers
{
    public static class ExtensionHelpers
    {
        public static string Cut(this string str, int maxLength)
        {
            if (maxLength < 5)
                maxLength = 5;

            if (str.Length > maxLength)
            {
                return str.Substring(0, maxLength - 2) + "..";

            }
            return str;
        }

        public static string StripTags(this string str)
        {
            str = HttpUtility.HtmlDecode(str);
            str = HttpUtility.HtmlDecode(str);
            return Regex.Replace(str, "<.*?>", string.Empty);
        }

        public static string StripSpaces(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"\s+", "");
        }

        public static string StripDoubleSpaces(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"\s+", " ");
        }

        public static string HtmlToText(this string str)
        {
            str = HttpUtility.HtmlDecode(str);
            str = str.StripDoubleSpaces();
            str = str.Replace("</p>", "\r\n");
            str = Regex.Replace(str, "<br.*>", "\r\n");
            return str.StripTags();
        }

        public static int ToInt(this int? value, int defaultInt)
        {
            if (value == null)
                return defaultInt;
            return value.Value;
        }

        public static int ToInt(this string value, int defaultInt)
        {
            if (string.IsNullOrEmpty(value))
                return defaultInt;

            int res;

            if (int.TryParse(value, out res))
                return res;

            return defaultInt;
        }

        public static decimal ToDecimal(this decimal? value, decimal defaultDecimal)
        {
            if (value == null)
                return defaultDecimal;
            return value.Value;
        }


    }
}