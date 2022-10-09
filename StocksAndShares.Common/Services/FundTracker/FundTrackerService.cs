using StocksAndShares.Api.Gateway.Services.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace StocksAndShares.Api.Gateway.Services.FundTracker
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
