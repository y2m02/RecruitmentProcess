using System.Globalization;

namespace RecruitmentManagementApi.Models.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string str)
        {
            return
                string.IsNullOrEmpty(str) ||
                string.IsNullOrWhiteSpace(str);
        }

        public static bool HasValue(this string str)
        {
            return !IsEmpty(str);
        }

        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static int ToIntOrDefault(this string str)
        {
            return int.TryParse(str, out var number)
                ? number
                : 0;
        }

        public static string Format(this string str, params object[] values)
        {
            return string.Format(str, values);
        }

        public static decimal ToDecimal(this string str)
        {
            return decimal.Parse(str);
        }

        public static decimal ToDecimalOrDefault(this string str)
        {
            return decimal.TryParse(str, NumberStyles.Number, new CultureInfo("en-US"), out var number)
                ? number
                : 0.00m;
        }
    }
}
