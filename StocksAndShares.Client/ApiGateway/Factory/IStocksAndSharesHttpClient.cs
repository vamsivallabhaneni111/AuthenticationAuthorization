using System.Threading.Tasks;

namespace StocksAndShares.Client.ApiGateway.Factory
{
    public interface IStocksAndSharesHttpClient
    {
        Task<TResult> GetApiAsync<TResult>(string route_url);
    }
}
