using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentManagementApi.Models.Extensions
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> executor)
        {
            foreach (var item in enumerable)
            {
                executor(item);
            }
        }

        public static void Each<T>(this IEnumerable<T> enumerable, Action<T, int> executor)
        {
            var index = 0;

            foreach (var item in enumerable)
            {
                executor(item, index++);
            }
        }

        public static List<R> EagerSelect<T, R>(this IEnumerable<T> list, Func<T, R> selector)
        {
            return list.Select(selector).ToList();
        }

        public static List<T> PrettyWhere<T>(this IEnumerable<T> list, Func<T, bool> filter)
        {
            return list.Where(filter).ToList();
        }
    }
}