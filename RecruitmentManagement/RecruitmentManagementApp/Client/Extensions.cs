using System.Threading.Tasks;

namespace RecruitmentManagementApp.Client
{
    public static class Extensions
    {
        /// <summary>
        ///     Get the JSON decoded body of the response.
        /// </summary>
        public static async Task<T> Json<T>(this Task<Response> task, bool dispose = true)
        {
            if (dispose)
            {
                using var response = await task.ConfigureAwait(false);

                return await response.JsonAsync<T>().ConfigureAwait(false);
            }
            else
            {
                var response = await task.ConfigureAwait(false);

                return await response.JsonAsync<T>().ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Get the body of the response.
        /// </summary>
        public static async Task<string> Body(this Task<Response> task, bool dispose = true)
        {
            if (dispose)
            {
                using var response = await task.ConfigureAwait(false);

                return await response.BodyAsync().ConfigureAwait(false);
            }
            else
            {
                var response = await task.ConfigureAwait(false);

                return await response.BodyAsync().ConfigureAwait(false);
            }
        }
    }
}
