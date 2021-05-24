#nullable enable
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RecruitmentManagementApp.Client
{
    public partial record Http
    {
        #region Sync overloads

        /// <summary>
        ///     Issue a DELETE request to the given URL.
        /// </summary>
        public Response Delete(string url) => Send(HttpMethod.Delete, url);

        /// <summary>
        ///     Issue a GET request to the given URL.
        /// </summary>
        public Response Get(string url) => Send(HttpMethod.Get, url);

        /// <summary>
        ///     Issue a HEAD request to the given URL.
        /// </summary>
        public Response Head(string url) => Send(HttpMethod.Head, url);

        /// <summary>
        ///     Issue a POST request to the given URL.
        /// </summary>
        public Response Post(string url) => Send(HttpMethod.Post, url);

        /// <summary>
        ///     Issue a PUT request to the given URL.
        /// </summary>
        public Response Put(string url) => Send(HttpMethod.Put, url);

        /// <summary>
        ///     Issue a GET request to the given URL and decode it to <typeparamref name="T" />.
        /// </summary>
        public T? Get<T>(string url) => Get(url).Json<T>();

        /// <summary>
        ///     Issue a GET request to the given URL and decode it to <typeparamref name="T" />.
        /// </summary>
        public Task<T> GetAsync<T>(string url, bool dispose = true)
        {
            return GetAsync(url).Json<T>(dispose);
        }

        /// <summary>
        ///     Issue a POST request to the given URL and decode it to <typeparamref name="T" />.
        /// </summary>
        public T? Post<T>(string url) => Post(url).Json<T>();

        /// <summary>
        ///     Issue a POST request to the given URL and decode it to <typeparamref name="T" />.
        /// </summary>
        public Task<T> PostAsync<T>(string url, bool dispose = true)
        {
            return PostAsync(url).Json<T>(dispose);
        }

        #endregion

        #region Modifiers

        /// <summary>
        ///     Specify the base URL for subsequent requests.
        /// </summary>
        public Http WithBaseUrl(string url)
        {
            return this with { BaseUrl = url };
        }

        /// <summary>
        ///     Specify the basic authentication username and password for the subsequent requests.
        /// </summary>
        public Http WithBasicAuth(string username, string password)
        {
            return WithHeader(
                "Authorization",
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))}"
            );
        }

        /// <summary>
        ///     Attach a JSON body to the subsequent requests.
        /// </summary>
        public Http WithBody<T>(T value)
        {
            return WithJsonBody(json: JsonConvert.SerializeObject(value));
        }

        /// <summary>
        ///     Attach a JSON body to the subsequent requests.
        /// </summary>
        public Http WithJsonBody(string json)
        {
            return WithBody(content: new JsonContent(json));
        }

        /// <summary>
        ///     Attach a raw body to the subsequent requests.
        /// </summary>
        public Http WithBody(HttpContent content)
        {
            return this with { Content = content };
        }

        /// <summary>
        ///     Specify an <see cref="System.Net.Http.HttpClient" /> to use for subsequent requests.
        /// </summary>
        /// <returns></returns>
        public Http WithClient(Func<HttpClient> client)
        {
            return this with { ClientFactory = client };
        }

        /// <summary>
        ///     Add the given header to the subsequent requests.
        /// </summary>
        public Http WithHeader(string name, params string[] values)
        {
            var instance = this with
            {
                Headers = new(Headers),
            };

            instance.Headers[name] = new(name, values);

            return instance;
        }

        /// <summary>
        ///     Add the given header in its raw state to the subsequent requests.
        /// </summary>
        public Http WithRawHeader(string name, params string[] values)
        {
            var instance = this with
            {
                Headers = new(Headers),
            };

            instance.Headers[name] = new(name, values, validate: false);

            return instance;
        }

        /// <summary>
        ///     Specify the number of times the subsequent requests should be attempted.
        /// </summary>
        public Http WithRetry(int times, TimeSpan delay)
        {
            return this with { RetryTimes = times, RetryDelay = delay };
        }

        /// <summary>
        ///     Specify the number of times the subsequent requests should be attempted.
        /// </summary>
        /// <param name="seconds">The delay (in seconds)</param>
        public Http WithRetry(int times, int seconds)
        {
            return WithRetry(times, TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        ///     Specify the timeout for the subsequent requests.
        /// </summary>
        public Http WithTimeout(TimeSpan timeout)
        {
            return this with { Timeout = timeout };
        }

        /// <summary>
        ///     Specify the timeout (in seconds) for the subsequent requests.
        /// </summary>
        public Http WithTimeout(int seconds)
        {
            return WithTimeout(TimeSpan.FromSeconds(seconds));
        }

        #endregion
    }
}
#nullable restore
