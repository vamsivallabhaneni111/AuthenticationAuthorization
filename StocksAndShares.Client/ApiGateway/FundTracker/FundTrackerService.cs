using StocksAndShares.Client.ApiGateway.Factory;
using System.Threading.Tasks;

namespace StocksAndShares.Client.ApiGateway.FundTracker
{
    public class FundTrackerService : IFundTrackerService
    {
        private const string base_url = "https://localhost:44368/api"; //move this to appsettings.json
        private readonly IStocksAndSharesHttpClient _stocksAndSharesHttpClient;

        public FundTrackerService(IStocksAndSharesHttpClient stocksAndSharesHttpClient)
        {
            _stocksAndSharesHttpClient = stocksAndSharesHttpClient;
        }
        public async Task<string> GetIntelFundAmount()
        {
            return await _stocksAndSharesHttpClient.GetApiAsync<string>(base_url + "/FundTracker/GetIntelFundAmount");
        }
    }
}
