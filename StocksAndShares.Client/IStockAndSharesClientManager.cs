using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StocksAndShares.Client
{
    public interface IStockAndSharesClientManager
    {
        Task<HttpResponseMessage> AccessTokenRefreshWrapper(Func<Task<HttpResponseMessage>> securedRequest);
        Task<TResult> GetResourceAsync<TResult>(string url);
    }
}