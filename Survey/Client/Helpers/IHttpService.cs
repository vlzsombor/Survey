﻿using Survey.Client.Repository;
using System.Threading.Tasks;

namespace Survey.Client.Helpers
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url);

        public Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
        public Task<HttpResponseWrapper<object>> Put<T>(string url, T data);
        Task<HttpResponseWrapper<object>> Delete(string url);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);


    }
}
