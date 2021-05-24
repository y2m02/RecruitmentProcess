#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RecruitmentManagementApp.Client
{
    public class Response : IDisposable
    {
        public Response(HttpResponseMessage response)
        {
            HttpResponse = response;
            RequestUri = response.RequestMessage.RequestUri;
        }

        protected internal HttpResponseMessage HttpResponse { get; set; }

        /// <summary>
        ///     Get the url of the request.
        /// </summary>
        public Uri RequestUri { get; }

        /// <summary>
        ///     Determine if the response indicates a client error occurred.
        /// </summary>
        public bool ClientError => Status is >= 400 and < 500;

        /// <summary>
        ///     Determine if the response indicates a client or server error occurred.
        /// </summary>
        public bool Failed => ClientError || ServerError;

        /// <summary>
        ///     Get the headers from the response.
        /// </summary>
        public HttpResponseHeaders Headers => HttpResponse.Headers;

        /// <summary>
        ///     Determine if the response was a redirect.
        /// </summary>
        public bool Redirect => Status is >= 300 and < 400;

        /// <summary>
        ///     Determine if the response indicates a server error occurred.
        /// </summary>
        public bool ServerError => Status >= 500;

        /// <summary>
        ///     Get the status code of the response.
        /// </summary>
        public int Status => (int)HttpResponse.StatusCode;

        /// <summary>
        ///     Determine if the request was successful.
        /// </summary>
        public bool Successful => Status is >= 200 and < 300;

        /// <inheritdoc />
        public void Dispose()
        {
            HttpResponse?.Dispose();
        }

        /// <summary>
        ///     Get the body of the response.
        /// </summary>
        public Task<string> BodyAsync()
        {
            var readAsStringAsync = HttpResponse.Content.ReadAsStringAsync();

            return readAsStringAsync;
        }

        /// <summary>
        ///     Get the body of the response.
        /// </summary>
        public Task<byte[]> BytesAsync()
        {
            return HttpResponse.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        ///     Get the body of the response.
        /// </summary>
        public Task<Stream> StreamAsync()
        {
            return HttpResponse.Content.ReadAsStreamAsync();
        }

        /// <summary>
        ///     Get the body of the response.
        /// </summary>
        public string Body()
        {
            return BodyAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        ///     Get a header from the response.
        /// </summary>
        public IEnumerable<string> Header(string key)
        {
            return Headers.GetValues(key);
        }

        /// <summary>
        ///     Execute the given callback if there was a server or client error.
        /// </summary>
        public Response OnError(Action<Response> executor, Func<Response, bool>? condition = null)
        {
            if (Failed && condition?.Invoke(this) != false)
            {
                executor(this);
            }

            return this;
        }

        /// <summary>
        ///     Throw an exception if a server or client error occurred.
        /// </summary>
        /// <exception cref="HttpException">The HTTP response is unsuccessful.</exception>
        public Response EnsureSuccess()
        {
            if (!Successful)
            {
                throw new HttpException(this);
            }

            return this;
        }

        /// <summary>
        ///     Get the JSON decoded body of the response.
        /// </summary>
        public async Task<T> JsonAsync<T>()
        {
            return JsonConvert.DeserializeObject<T>(await BodyAsync().ConfigureAwait(false));
        }

        /// <summary>
        ///     Get the JSON decoded body of the response.
        /// </summary>
        public T Json<T>()
        {
            return JsonAsync<T>().GetAwaiter().GetResult();
        }

        public override string ToString()
        {
            return $"{(Successful ? "Successful" : "Unsuccessful")} response from {RequestUri}";
        }
    }
}
#nullable restore
