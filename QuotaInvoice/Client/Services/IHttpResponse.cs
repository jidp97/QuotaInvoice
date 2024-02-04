﻿using System.Threading.Tasks;

namespace QuotaInvoice.Client.Services
{
    public interface IHttpResponse
    {
        Task<HttpResponseWrapper<object>> Delete(string url);

        Task<HttpResponseWrapper<T>> Get<T>(string url);

        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);

        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);

        Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar);
    }
}