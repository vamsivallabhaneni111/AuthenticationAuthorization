using StocksAndShares.Api.Gateway.Services.Factory;

namespace StocksAndShares.Api.Gateway.Services.LiquidFunds
{
    public class LiquidFundsService : ILiquidFundsService
    {
        private const string base_url = "https://localhost:44389/api"; //move this to appsettings.json
        private readonly IStocksAndSharesHttpClient _stocksAndSharesHttpClient;

        public LiquidFundsService(IStocksAndSharesHttpClient stocksAndSharesHttpClient)
        {
            _stocksAndSharesHttpClient = stocksAndSharesHttpClient;
        }

        public async Task<string> GetCurrencyAtLocation()
        {
           return await _stocksAndSharesHttpClient.GetApiAsync<string>(base_url+"/GeoCurrency/GetCurrencyAtLocation");
        }
    }
}
