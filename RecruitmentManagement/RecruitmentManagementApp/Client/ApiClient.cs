﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HelpersLibrary.ApiClientHelper;
using Microsoft.Extensions.Configuration;

namespace RecruitmentManagementApp.Client
{
    public interface IApiClient
    {
        Task<TResponse> Get<TResponse>(string resource);
        Task<TResponse> Post<TResponse>(string resource, object body = null);
        Task Put<TResponse>(string resource, object body = null);
        Task<TResponse> Delete<TResponse>(string resource);
    }

    public class ApiClient : IApiClient
    {
        private readonly Http client = Http.Make();

        public ApiClient(IConfiguration configuration)
        {
            client = client.WithBaseUrl(configuration.GetSection("API_URL").Value);
        }

        public Task<TResponse> Get<TResponse>(string resource)
        {
            return Execute<TResponse>(resource, client.GetAsync);
        }

        public Task<TResponse> Post<TResponse>(string resource, object body = null)
        {
            return Execute<TResponse>(resource, client.WithBody(body).PostAsync);
        }

        public Task Put<TResponse>(string resource, object body = null)
        {
            return Execute<TResponse>(resource, client.WithBody(body).PutAsync);
        }

        public Task<TResponse> Delete<TResponse>(string resource)
        {
            return Execute<TResponse>(resource, client.DeleteAsync);
        }

        private static async Task<TResponse> Execute<TResponse>(string resource, Func<string, Task<Response>> executor)
        {
            using var response = await executor(resource).ConfigureAwait(false);

            if (response.Successful)
            {
                return await response.JsonAsync<TResponse>().ConfigureAwait(false);
            }

            var message = await response.BodyAsync().ConfigureAwait(false);

            throw response.Status switch
            {
                400 => new ValidationException(message),
                _ => new Exception(message),
            };
        }
    }
}
