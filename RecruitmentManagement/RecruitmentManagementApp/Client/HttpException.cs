using System;

namespace RecruitmentManagementApp.Client
{
    public class HttpException : Exception
    {
        public HttpException(Response response) : base(MakeMessage(response))
        {
            Status = response.Status;
            Url = response.RequestUri;
            Response = response.Body();
        }

        /// <summary>
        /// Get the body of the response.
        /// </summary>
        public string? Response { get; }

        /// <summary>
        /// Get the status code of the response.
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// Get the url of the request.
        /// </summary>
        public Uri Url { get; }

        private static string MakeMessage(Response response)
        {
            return $"Unsuccessful response from {response.RequestUri}:{Environment.NewLine}{response.Body()}";
        }
    }
}
