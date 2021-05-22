using System.Globalization;

namespace RecruitmentManagementApi.Models.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToStringWithDecimals(this decimal number, int decimals = 2)
        {
            return number.ToString($"N{decimals}", new CultureInfo("en-US"));
        }

        public static string ToStringWithDecimals(this decimal? number, int decimals = 2)
        {
            return number.GetValueOrDefault().ToStringWithDecimals(decimals);
        }
    }
}