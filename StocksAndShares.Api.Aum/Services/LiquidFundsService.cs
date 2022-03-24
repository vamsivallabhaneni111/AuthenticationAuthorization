using StocksAndShares.Api.Aum.Services.Factory;
using System.Threading.Tasks;

namespace StocksAndShares.Api.Aum.Services
{
    public class LiquidFundsService : ILiquidFundsService
    {
        private readonly IStocksAndSharesHttpClient _httpClient;

        public LiquidFundsService(IStocksAndSharesHttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<string> CurrencyAtLocation()
        {
            var response = await _httpClient.GetApiAsync<string>("/GeoCurrency/CurrencyAtLocation");
            return response;
        }
    }
}
