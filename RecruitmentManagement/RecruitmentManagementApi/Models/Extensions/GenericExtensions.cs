using System;

namespace RecruitmentManagementApi.Models.Extensions
{
    public static class GenericExtensions
    {
        public static bool HasValue<T>(this T @object)
        {
            return @object is not null;
        }

        public static T Tap<T>(this T @object, Action<T> executor)
        {
            executor(@object);

            return @object;
        }
    }
}