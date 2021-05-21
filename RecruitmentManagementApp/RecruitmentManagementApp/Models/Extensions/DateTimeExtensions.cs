using System;

namespace RecruitmentManagementApp.Models.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsGreaterThan(this DateTime dateTime, DateTime dateTime2, bool includeTime = false)
        {
            return includeTime 
                ? dateTime > dateTime2 
                : dateTime.Date > dateTime2.Date;
        }

        public static bool IsGreaterOrEqualTo(this DateTime dateTime, DateTime dateTime2, bool includeTime = false)
        {
            return includeTime 
                ? dateTime >= dateTime2 
                : dateTime.Date > dateTime2.Date;
        }

        public static bool IsLessThan(this DateTime dateTime, DateTime dateTime2, bool includeTime = false)
        {
            return includeTime 
                ? dateTime < dateTime2 
                : dateTime.Date < dateTime2.Date;
        }

        public static bool IsLessOrEqualTo(this DateTime dateTime, DateTime dateTime2, bool includeTime = false)
        {
            return includeTime 
                ? dateTime <= dateTime2 
                : dateTime.Date <= dateTime2.Date;
        }
    }
}