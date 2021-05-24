#nullable enable
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentManagementApp.Client
{
    /// <summary>
    ///     An expressive, minimal API around the <see cref="System.Net.Http.HttpClient" />
    ///     See /src/Http/README.md
    /// </summary>
    public partial record Http
    {
        /// <summary>
        ///     The default HTTP client used when calling <see cref="SendAsync" /> with no client instance.
        /// </summary>
        protected internal static readonly Lazy<HttpClient> Client = new(
            () => new(new HttpClientHandler { AllowAutoRedirect = true }, disposeHandler: true)
        );

        /// <summary>
        ///     The base URL for the request.
        /// </summary>
        protected internal string? BaseUrl { get; set; } = null;

        /// <summary>
        ///     The body for the request.
        /// </summary>
        protected internal HttpContent? Content { get; set; } = null;

        /// <summary>
        ///     Resolves an <see cref="System.Net.Http.HttpClient" /> to use when making subsequent requests.
        /// </summary>
        protected internal Func<HttpClient> ClientFactory { get; set; } = () => Client.Value;

        /// <summary>
        ///     The headers for the request.
        /// </summary>
        protected internal Dictionary<string, RequestHeader> Headers { get; set; } = new()
        {
            ["Accept"] = new(name: "Accept", values: new[] { "application/json" }),
        };

        /// <summary>
        ///     The time to wait between retries.
        /// </summary>
        protected internal TimeSpan? RetryDelay { get; set; }

        /// <summary>
        ///     The number of times to try the request.
        /// </summary>
        protected internal int? RetryTimes { get; set; }

        /// <summary>
        ///     The time to wait before the request times out.
        /// </summary>
        protected internal TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

        #region Getters

        protected internal HttpRequestMessage MakeRequestMessage(HttpMethod method, string? url)
        {
            var request = new HttpRequestMessage(method, new Uri(UrlHelper.Combine(BaseUrl, url)));

            foreach (var header in Headers)
            {
                header.Value.Attach(request.Headers);
            }

            if (Content != null)
            {
                request.Content = Content;
            }

            return request;
        }

        #endregion

        #region Constructors

        private Http() { }

        /// <summary>
        ///     Create a new <see cref="Http" /> instance.
        /// </summary>
        public static Http Make() => new();

        #endregion

        #region Executors

        /// <summary>
        ///     Issue a DELETE request to the given URL.
        /// </summary>
        /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
        public Task<Response> DeleteAsync(string url)
        {
            return SendAsync(HttpMethod.Delete, url);
        }

        /// <summary>
        ///     Issue a GET request to the given URL.
        /// </summary>
        /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
        public Task<Response> GetAsync(string url)
        {
            return SendAsync(HttpMethod.Get, url);
        }

        /// <summary>
        ///     Issue a HEAD request to the given URL.
        /// </summary>
        /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
        public Task<Response> HeadAsync(string url)
        {
            return SendAsync(HttpMethod.Head, url);
        }

        /// <summary>
        ///     Issue a POST request to the given URL.
        /// </summary>
        /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
        public Task<Response> PostAsync(string url)
        {
            return SendAsync(HttpMethod.Post, url);
        }

        /// <summary>
        ///     Issue a PUT request to the given URL.
        /// </summary>
        /// <exception cref="HttpRequestException">The HTTP response is unsuccessful.</exception>
        public Task<Response> PutAsync(string? url = null)
        {
            return SendAsync(HttpMethod.Put, url);
        }

        /// <summary>
        ///     Send the request to the given URL.
        /// </summary>
        /// <exception cref="HttpRequestException">
        ///     The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate
        ///     validation or timeout.
        /// </exception>
        protected internal async Task<Response> SendAsync(HttpMethod method, string? url = null)
        {
            var attemptsLeft = RetryTimes ?? 1;

            while (true)
            {
                try
                {
                    using var request = MakeRequestMessage(method, url);

                    using var content = request.Content;

                    using var canceller = new CancellationTokenSource(Timeout);

                    return new(await ClientFactory.Invoke().SendAsync(request, canceller.Token).ConfigureAwait(false));
                }
                catch (HttpRequestException) when (--attemptsLeft > 0)
                {
                    await Task.Delay(RetryDelay!.Value).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        ///     Send the request to the given URL.
        /// </summary>
        /// <exception cref="HttpRequestException">
        ///     The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate
        ///     validation or timeout.
        /// </exception>
        protected internal Response Send(HttpMethod method, string? url = null)
        {
            var attemptsLeft = RetryTimes ?? 1;

            while (true)
            {
                try
                {
                    using var request = MakeRequestMessage(method, url);

                    using var content = request.Content;

                    using var canceller = new CancellationTokenSource(Timeout);

                    return new(ClientFactory().SendAsync(request, canceller.Token).Result);
                }
                catch (HttpRequestException) when (--attemptsLeft > 0)
                {
                    Task.Delay(RetryDelay!.Value).Wait();
                }
            }
        }

        #endregion
    }
}
#nullable restore
