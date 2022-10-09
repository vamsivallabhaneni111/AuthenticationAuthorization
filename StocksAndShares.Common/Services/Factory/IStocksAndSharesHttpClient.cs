namespace StocksAndShares.Api.Gateway.Services.Factory
{
    public interface IStocksAndSharesHttpClient
    {
        Task<TResult> GetApiAsync<TResult>(string route_url);
    }
}
