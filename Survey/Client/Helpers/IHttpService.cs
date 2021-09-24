using Survey.Client.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Client.Helpers
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url);

        public Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
        public Task<HttpResponseWrapper<object>> Put<T>(string url, T data);

    }
}
