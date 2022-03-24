using System.Threading.Tasks;

namespace StocksAndShares.Api.Aum.Services.Factory
{
    public interface IStocksAndSharesHttpClient
    {
        Task<TResult> GetApiAsync<TResult>(string route_url);
    }
}
