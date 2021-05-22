namespace RecruitmentManagementApi.Models.Extensions
{
    public static class GenericExtensions
    {
        public static bool HasValue<T>(this T @object)
        {
            return @object is not null;
        }
    }
}